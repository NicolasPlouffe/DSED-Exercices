using Microsoft.VisualBasic;
using RabbitMQ.Client;
using System;
using System.Text;

namespace DSED_M07_TraitementCommande_producteur
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Article> listArticle = new List<Article>();
            Article art1 = new Article(
                new Guid(),
                "patate",
                1000,
                1);

            Commande nouvelleCommande = new Commande(
                new Guid(),
                Type.normal,
                "Jonny",
                listArticle);

            Commande nouvelleCommande2 = new Commande(
                new Guid(),
                Type.premium,
                "George",
                listArticle);

            /*Random rnd = new Random(DateTime.Now.Millisecond);
            int nombreMessages = 10;
            string[] vitesses = { "lent", "moyen", "rapide" };
            string[] couleurs = { "orange", "roux", "noir", "brun", "blanc" };
            string[] types = { "lapin", "chien" };*/

            string[] commande = { "commande" };

            string[] statue = { "place", "attente" };

            string[] type = { "normal", "premium" };



            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {

                    channel.ExchangeDeclare(
                    exchange: "commandes",
                    type: "topic",
                    durable: true,
                    autoDelete: false
                    );

                    Console.WriteLine(" Press [enter] to begin.");
                    Console.ReadLine();

                    /* for (int i = 0; i < nombreMessages; i++)
                     {
                         string vitesse = vitesses[rnd.Next(vitesses.Length)];
                         string couleur = couleurs[rnd.Next(couleurs.Length)];
                         string type = types[rnd.Next(types.Length)];

                         string sujet = $"{vitesse}.{couleur}.{type}";
                         string message = $"L'animal {type} est {couleur} et est {vitesse}";

                         var body = Encoding.UTF8.GetBytes(message);
                         channel.BasicPublish(exchange: "information_animaux",
                         routingKey: sujet,
                         basicProperties: null,
                         body: body);

                         Console.Out.WriteLine($"Message \"{message}\" dans le sujet {sujet}");
                     }*/

                    string sujet = $"{commande}.{statue}.{type}";
                    string message = $"La {commande} est {statue} {type} ";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "information_commandes",
                                             routingKey: sujet,
                                             basicProperties: null,
                                             body: body);

                    Console.Out.WriteLine($"Message \"{message}\" dans le sujet {sujet}");
                }
            }
        }
    }
}