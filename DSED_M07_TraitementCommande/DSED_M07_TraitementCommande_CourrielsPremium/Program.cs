using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using DSED_M07_TraitementCommande_producteur;

namespace DSED_M07_TraitementCommande_CourrielsPremium
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] requetesSujets = { "commande.placee.premium" };
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
                    "m07-courriel-premium",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: "m07-courriel-premium",
                        exchange: "m07-commandes",
                        routingKey: requeteSujet);
                    }

                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        string sujet = ea.RoutingKey;
                        
                        var objDeserialised = JsonSerializer.Deserialize<Commande>(message);
                        
                        Console.WriteLine($"Commande premium : {objDeserialised.NoReferenceCommande}");
                    };
                    channel.BasicConsume(queue: "m07-courriel-premium",
                    autoAck: true,
                    consumerTag: "m07-courriel-premium",
                    consumer: consumateur);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}