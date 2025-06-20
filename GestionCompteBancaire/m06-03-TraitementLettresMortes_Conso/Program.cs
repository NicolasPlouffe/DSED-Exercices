using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class Program
{
    private const string _deadLetterQueue = "m06-comptes-lettres-mortes";
    private const string _errorDirectory = "TransactionsEnErreur";

    public static void Main()
    {
        Directory.CreateDirectory(_errorDirectory);
        
        var factory = new ConnectionFactory() { HostName = "localhost" };
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(_deadLetterQueue, durable: true, exclusive: false, autoDelete: false);
        
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) => SaveToFile(ea.Body.ToArray());
        
        channel.BasicConsume(_deadLetterQueue, autoAck: true, consumer);
        Console.ReadLine();
    }

    private static void SaveToFile(byte[] data)
    {
        var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        var guid = Guid.NewGuid().ToString("N");
        var filename = $"{timestamp}_{guid}.bin";
        var path = Path.Combine(_errorDirectory, filename);
        
        File.WriteAllBytes(path, data);
        Console.WriteLine($"Fichier sauvegardé : {filename}");
    }
}