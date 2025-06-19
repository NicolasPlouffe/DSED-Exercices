using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace DSED_M07_TraitementCommande_journal
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] requetesSujets = { "#" };
            
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(
                    exchange: "m07-commandes",
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );
                    channel.QueueDeclare(
                    "m07-journal",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: "m07-journal",
                        exchange: "m07-commandes",
                        routingKey: requeteSujet);
                    }

                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        string sujet = ea.RoutingKey;
                        
                        string nomFichier = $"{st}"
                        Console.WriteLine($"Message reçu \"{message}\" avec le sujet : {sujet}");
                    };
                    channel.BasicConsume(queue: "m07-journal",
                    autoAck: true,
                    consumerTag: "m07-journal",
                    consumer: consumateur);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}