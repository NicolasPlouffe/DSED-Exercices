using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DAL;
using Entite;
using Entite.Manipulations;
using ManipulationsBL;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static readonly string _fileNom = "m06-comptes";
    private static readonly string _fileLettresMortes = "m06-comptes-lettres-mortes";

    public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var serviceProvider = new ServiceCollection()
            .AddScoped<ManipulationCompteBLConsoProd>()
            .AddScoped<ManipulationTransactionBLConsoProd>()
            .BuildServiceProvider();


        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare("echange-lettres-mortes", ExchangeType.Direct);
        channel.QueueDeclare(_fileLettresMortes, true, false, false);
        channel.QueueBind(_fileLettresMortes, "echange-lettres-mortes", _fileLettresMortes);

        var arg = new Dictionary<string, object>
        {
            { "x-dead-letter-exchange", "echange-lettres-mortes" } 
        };

        channel.QueueDeclare(
            queue: _fileNom,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: arg
        );

        var consommateur = new EventingBasicConsumer(channel);
        consommateur.Received += (model, ea) => TraitementMesage(channel, ea,serviceProvider);
        
        channel.BasicConsume(_fileNom, autoAck: false, consommateur);
        Console.ReadLine();
    }

    private static void TraitementMesage(
        IModel canal,
        BasicDeliverEventArgs ea,
        IServiceProvider serviceProvider)
    {
        
        using var scope = serviceProvider.CreateScope();
        var _manipulationCompte = scope.ServiceProvider.GetRequiredService<ManipulationCompteBLConsoProd>();
        var _manipulationTransaction = scope.ServiceProvider.GetRequiredService<ManipulationTransactionBLConsoProd>();

        try
        {
            var enveloppe = JsonSerializer.Deserialize<MessageEnveloppe>(ea.Body.ToArray());

            if (string.IsNullOrEmpty(enveloppe.Action) ||
                enveloppe.DataEntiteEncodees is null ||
                enveloppe.DataEntiteEncodees.Length == 0)
            {
                throw new InvalidOperationException("Enveloppe invalide");
            }

            var json = Encoding.UTF8.GetString(enveloppe.DataEntiteEncodees);
            switch (enveloppe.Action)
            {
                case "PostCompte":
                    var compte = JsonSerializer.Deserialize<CompteEntite>(json);
                    _manipulationCompte.AjouterCompteSQLServer(compte);
                    break;

                case "PutCompte":
                    var compteMaj = JsonSerializer.Deserialize<CompteEntite>(json);
                    _manipulationCompte.ModifierCompteSQLServer(compteMaj);
                    break;

                case "PostTransaction":
                    var transaction = JsonSerializer.Deserialize<TransactionEntite>(json);
                    _manipulationTransaction.ModifierTransactionSQLServer(transaction);
                    break;

                case "PutTransaction":
                    var transactionMAJ = JsonSerializer.Deserialize<TransactionEntite>(enveloppe.TypeEntite);
                    _manipulationTransaction.ModifierTransactionSQLServer(transactionMAJ);
                    break;
            }

            canal.BasicAck(ea.DeliveryTag, false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur: {ex.Message}");
            
            canal.BasicNack(ea.DeliveryTag, false, true);
        }
    }

};