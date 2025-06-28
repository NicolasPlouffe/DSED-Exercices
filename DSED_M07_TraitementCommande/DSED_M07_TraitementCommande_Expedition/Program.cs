using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using DSED_M07_TraitementCommande_producteur;

namespace DSED_M07_TraitementCommande_Expedition
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] requetesSujets = { "commande.place.*" };
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
                    "m07-preparation-expedition",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: "m07-preparation-expedition",
                        exchange: "m07-commandes",
                        routingKey: requeteSujet);
                    }

                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        string sujet = ea.RoutingKey;
                        Commande objDeserialise = JsonSerializer.Deserialize<Commande>(message);
                        Console.WriteLine("Préparez les articles suivants : ");
                        foreach (var article in objDeserialise.listArticles)
                        {
                            Console.WriteLine(article.Nom);
                        }
                        
                        Console.WriteLine($"L'emballage est de type : {objDeserialise.TypeEnvoie}");
                    };
                    channel.BasicConsume(queue: "m07-preparation-expedition",
                    autoAck: true,
                    consumerTag: "m07-preparation-expedition",
                    consumer: consumateur);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}