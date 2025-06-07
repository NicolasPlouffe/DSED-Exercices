using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Text.Json;
using M06_CasUtilisation_Clients;
using M06_MessageClient;


namespace M06_Clients_Consommateur
{
    class Program
    {
        private static ManualResetEvent waitHandle = new ManualResetEvent(false);
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (IModel channel = connexion.CreateModel())
                {
                    channel.QueueDeclare(queue: "m06-clients", durable: false, exclusive: false,
                    autoDelete: false, arguments: null
                    );

                    EventingBasicConsumer consommateur = new EventingBasicConsumer(channel);
                    consommateur.Received += (model, ea) =>
                    {
                        byte[] donnees = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(donnees);
                        Console.WriteLine("Pre deserialisation");
                        MessageClient? messageRecu = JsonSerializer.Deserialize<MessageClient>(message);
                        Console.Out.WriteLine("Deserialisation succes");
                        ClientEntite client = new(
                            Guid.Empty
                            ,messageRecu.Prenom
                            ,messageRecu.Nom
                            ,messageRecu.Courriel
                            ,messageRecu.NumeroTelephone
                            );

                        Console.WriteLine("Recu avec succces");
                        channel.BasicAck(ea.DeliveryTag, false);
                    };
                    channel.BasicConsume(queue: "m06-clients",
                    autoAck: false,
                    consumer: consommateur
                    );
                    waitHandle.WaitOne();
                }
            }
        }
    }
}