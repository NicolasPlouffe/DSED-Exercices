using Microsoft.VisualBasic;
using RabbitMQ.Client;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;

namespace DSED_M07_TraitementCommande_producteur
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string[] clients = { "Nicolas", "Alexandre", "Marissa", "Jojo" };
            string[] typesCommande = { "normal", "premium" };
            
            List<Article> articlesDisponibles = new List<Article>
            {
                new Article(Guid.NewGuid(), "Patate", 1000m, 1),
                new Article(Guid.NewGuid(), "Jujubes", 5m, 3),
                new Article(Guid.NewGuid(), "Réglisse", 3m, 3),
                new Article(Guid.NewGuid(), "Pommes", 1m, 2)
            };

            string[] commande = { "commande" };

            string[] statue = { "placee", "attente" };


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

                    Console.WriteLine(" Press [enter] to begin.");
                    Console.ReadLine();

                    Random rnd = new Random();
                    int nombreCommandes = 10;

                    for (int i = 0; i < nombreCommandes; i++)
                    {
                        // Création commande aléatoire
                        string client = clients[rnd.Next(clients.Length)];
                        string type = typesCommande[rnd.Next(typesCommande.Length)];
                        Guid reference = Guid.NewGuid();
                    
                        // Génération de 1 à 4 articles aléatoires
                        List<Article> articles = new List<Article>();
                        for (int j = 0; j < rnd.Next(1, 5); j++)
                        {
                            articles.Add(articlesDisponibles[rnd.Next(articlesDisponibles.Count)]);
                        }
                        

                        Commande nouvellCommande = new Commande(reference,type,client,articles);
                        
                    string sujet = $"{commande}.{statue}.{nouvellCommande.TypeEnvoie}";
                    string message = $"La {nouvellCommande.NoReferenceCommande}" +
                                     $" est : {nouvellCommande.StatusEnvoie} de type : {nouvellCommande.TypeEnvoie} " +
                                     $"et contiens : {nouvellCommande.listArticles.ToArray()}";
                    
                    //string json = JsonSerializer.Serialize(nouvellCommande);
                    
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "m07-commandes",
                                             routingKey: sujet,
                                             basicProperties: null,
                                             body: body);

                    Console.Out.WriteLine($"Message \"{message}\" dans le sujet {sujet}"); 
                    
                    Console.Out.WriteLine($"Commande : {nouvellCommande.NoReferenceCommande}");
                    }
                }
            }
        }
    }
}