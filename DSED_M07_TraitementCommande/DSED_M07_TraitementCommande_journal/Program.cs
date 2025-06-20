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
                    // Declaration de l'echange normalement au niveau de Azure
                 
                    channel.ExchangeDeclare(
                    exchange: "m07-commandes",
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );
                    // Declaration de la file
                    channel.QueueDeclare(
                    "m07-journal",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                    // Liaison de la file du présent consommateur avec l'Exchange
                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: "m07-journal",
                        exchange: "m07-commandes",
                        routingKey: requeteSujet);
                    }

                     // Creation du repertoire de stockage
                     string repertioreJournalisation = "Journal";
                     Directory.CreateDirectory(repertioreJournalisation);
                     
                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        string sujet = ea.RoutingKey;
                        
                        // Génération du nom de fichier pour l'enregistrement
                        DateTime maintenant = DateTime.Now;
                        string nomFichier = $"{maintenant:yy-MM-dd}_{maintenant:HH-mm-ss}_{Guid.NewGuid()}.json";
                        string cheminFichier = Path.Combine(repertioreJournalisation, nomFichier);
                        File.WriteAllText(cheminFichier, message);
                        
                        Console.WriteLine($"Message reçu et enregistré dans le dossier {cheminFichier}");
                    };
                    channel.BasicConsume(
                        queue: "m07-journal",
                        autoAck: true,
                        consumerTag: "m07-journal",
                        consumer: consumateur
                        );

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}