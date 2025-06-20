using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using DSED_M07_TraitementCommande_producteur;

namespace DSED_M07_TraitementCommande_facturation
{
    class Program
    {
        static void Main(string[] args)
        {

            decimal tauxEscompte = 0.05m;
            decimal TVQ = 9.975m;
            
            string[] requetesSujets = { "commande.place.*" };
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    
                    // Declaration de l'echange normalement au niveau de Azure
                    channel.ExchangeDeclare(
                    exchange: "information_animaux",
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );
                    
                    // Declaration de la file
                    channel.QueueDeclare(
                    "consommateur2",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );
                    
                    // Liaison de la file du présent consommateur avec l'Exchange
                    foreach (var requeteSujet in requetesSujets)
                    {
                        channel.QueueBind(queue: "consommateur2",
                        exchange: "information_animaux",
                        routingKey: requeteSujet);
                    }
                    
                    // Creation du repertoire de stockage
                    string repertioreEnregitrementFacturation = "Factures";
                    Directory.CreateDirectory(repertioreEnregitrementFacturation);
                    
                    EventingBasicConsumer consumateur = new EventingBasicConsumer(channel);
                    consumateur.Received += (model, ea) =>
                    {
                        byte[] body = ea.Body.ToArray();
                        string message = Encoding.UTF8.GetString(body);
                        string sujet = ea.RoutingKey;
                        
                        //Deserialisation du tableau de bytes en objet
                        var commandeRecue = JsonSerializer.Deserialize<Commande>(message);
                        
                        // Traitement et modification si besoin de l'objet si besoin
                        if (commandeRecue.TypeEnvoie == "premium")
                        {
                            //retirer 5 % au total de la commande 
                            foreach (var article in commandeRecue.listArticles)
                            {
                                article.Prix -= (article.Prix * tauxEscompte);
                            }
                        }
                        
                        // Calculer les taxes 
                        foreach (var article in commandeRecue.listArticles)
                        {
                            article.Prix += (article.Prix * TVQ);
                        }
                        
                        // Génération du nom de fichier pour l'enregistrement de la facture
                        DateTime maintenant = DateTime.Now;
                        string nomFichier = $"{maintenant:yy-MM-dd}_{maintenant:HH-mm-ss}_{commandeRecue.NoReferenceCommande}";
                        string cheminFichier = Path.Combine(repertioreEnregitrementFacturation, nomFichier);
                        File.WriteAllText(cheminFichier, JsonSerializer.Serialize(commandeRecue));
                        
                        Console.WriteLine($"Facture reçu et enregistré dans le dossier {cheminFichier}");
                    };
                    channel.BasicConsume(queue: "m07-journal",
                    autoAck: true,
                    consumerTag: "m07-journal",
                    consumer: consumateur);

                    Console.WriteLine("Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}