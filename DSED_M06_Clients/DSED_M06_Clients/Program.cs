using M06_MessageClient;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;


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
                    channel.QueueDeclare(queue: "m06-clients",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                    );

                    for (int i = 0; i < 1; i++)
                    {


                        Console.WriteLine("Entrez Nom :");
                        string nom = Console.ReadLine();

                        Console.WriteLine("Entrez Prenom :");
                        string prenom = Console.ReadLine();

                        Console.WriteLine("Entrez Courriel :");
                        string courriel = Console.ReadLine();

                        Console.WriteLine("Entrez Numero de Telephone :");
                        string telephone = Console.ReadLine();

                        MessageClient messageClient = new MessageClient(prenom,nom,courriel,telephone);
                        string jsonString = JsonSerializer.Serialize(messageClient);
                        Console.WriteLine("Message serialiser avec succes");

                        byte[] body = Encoding.UTF8.GetBytes(jsonString);
                        Console.WriteLine("Encoded");

                        channel.BasicPublish(exchange: "", routingKey: "m06-clients", body: body);
                    }
                }

            }
        }
    }
}