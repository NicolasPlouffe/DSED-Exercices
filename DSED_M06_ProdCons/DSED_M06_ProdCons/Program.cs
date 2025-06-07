using RabbitMQ.Client;
using System;
using System.Text;

namespace DSED_Module06_FileMessagesRabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connexion = factory.CreateConnection())
            {
                using (IModel channel = connexion.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",//le nom du canal de file de message
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                    for (int i = 0; i < 10; i++)
                    {
                        
                        string message = "mon message - " + Guid.NewGuid();
                        Console.WriteLine("Entrez votre message");
                        string messageInput = Console.ReadLine();

                        byte[] body = Encoding.UTF8.GetBytes(messageInput);

                        channel.BasicPublish(exchange: "", routingKey: "hello", body: body);
                    }
                }

            }
        }
    }
}