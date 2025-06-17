using System.Text;
using System.Text.Json;
using Entite;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using IModel = RabbitMQ.Client.IModel;

namespace DAL;
using Entite.IDepot;

public class Depot_RabbitMQ_SQLServeur:ICompteDepot,ITransactionDepot
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public Depot_RabbitMQ_SQLServeur(string hostName)
    {
        var factory = new ConnectionFactory() { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare(
            queue: "comptes",
            durable: true,
            exclusive: false,
            autoDelete:false,
            arguments: null
            );
    }
    
    #region Compte
    
    public void CreerCompte(CompteEntite p_compte)
    {
        if (p_compte == null){throw new ArgumentNullException("CompteEntite p_compte is null ne sera pas serializer");}
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(p_compte));
        
            _channel.BasicPublish(
            exchange:"comptes",
            routingKey:"comptes",
            basicProperties:null,
            body:body
            );
    }

    public CompteEntite ObtenirCompte(Guid p_compteId)
    {
        // Creation fil de reponse temporaire
        var replyQueue = _channel.QueueDeclare(
            queue: "comptes",
            exclusive:true,
            autoDelete:true);
        
        var consumer = new EventingBasicConsumer(_channel);
        var correlationId = Guid.NewGuid().ToString();
        
        // Prpriete du message
        var props = _channel.CreateBasicProperties();
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueue;
        
        //Publication de la demande
        
        var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(p_compteId));
        _channel.BasicPublish(
            exchange:"comptes",
            routingKey:"comptes.get",
            basicProperties:props,
            body:messageBytes
            );
        
        //Attente Synchrone de la reponse
        CompteEntite compte = null;
        var mre = new ManualResetEvent(false);

        consumer.Received += (model, ea) =>
        {
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                compte = JsonSerializer.Deserialize<CompteEntite>(
                    Encoding.UTF8.GetString(ea.Body.ToArray()));
                mre.Set();
            }
        };

        _channel.BasicConsume(
            queue: replyQueue.QueueName,
            autoAck: true,
            consumer: consumer
        );
mre.WaitOne(5000);
return compte ?? throw new KeyNotFoundException("Compte non trouvé");
    }

    public void MAJCompte(CompteEntite p_compte)
    {
        if (p_compte == null){throw new ArgumentNullException("CompteEntite p_compte is null ne sera pas serializer");}
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(p_compte));
        
        _channel.BasicPublish(
            exchange:"comptes",
            routingKey:"comptes",
            basicProperties:null,
            body:body
        );    }
    #endregion
    
    #region Transaction
    public void CreerTransaction(TransactionEntite p_transaction)
    {
if (p_transaction == null){throw new ArgumentNullException("TransactionEntite p_transaction is null");}

var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(p_transaction));

    _channel.BasicPublish(
    exchange:"",
    routingKey:"transactions",
    basicProperties:null,
    body:body);
    }

    public TransactionEntite LireTransaction(Guid p_transactionId)
    {
        // Creation fil de reponse temporaire
        var replyQueue = _channel.QueueDeclare(
            queue: "comptes",
            exclusive:true,
            autoDelete:true);
        
        // Configuration des proprietes
        var props = _channel.CreateBasicProperties();
        props.CorrelationId = p_transactionId.ToString();
        props.ReplyTo = replyQueue;
        
        // POublication de la demande
        
        _channel.BasicPublish(
            exchange:"comptes",
            routingKey:"transaction.get",
            body:Encoding.UTF8.GetBytes(p_transactionId.ToString()),
            basicProperties:props
            );
        // Attente de la reponse
        TransactionEntite transaction = null;
        var mre = new ManualResetEvent(false);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            if (ea.BasicProperties.CorrelationId == p_transactionId.ToString()) 
            {
                transaction = JsonSerializer.Deserialize<TransactionEntite>(ea.Body.ToArray());
                mre.Set();
            }
        };

        _channel.BasicConsume(
            queue: replyQueue.QueueName,
            autoAck: true,
            consumer: consumer
        );

        mre.WaitOne(5000);
        return transaction ?? throw new KeyNotFoundException("Transaction non trouv<UNK>");
    }

    public List<TransactionEntite> ListerToutesLesTransactions()
    {
        var replyQueue = _channel.QueueDeclare("", exclusive: true, autoDelete: true);
        var correlationId = Guid.NewGuid().ToString();
    
        var props = _channel.CreateBasicProperties();
        props.CorrelationId = correlationId;
        props.ReplyTo = replyQueue.QueueName;

        _channel.BasicPublish(
            exchange: "",
            routingKey: "transactions.list",
            body: Array.Empty<byte>(),
            basicProperties: props
        );

        List<TransactionEntite> transactions = new();
        var mre = new ManualResetEvent(false);
    
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            if (ea.BasicProperties.CorrelationId == correlationId)
            {
                transactions = JsonSerializer.Deserialize<List<TransactionEntite>>(ea.Body.ToArray());
                mre.Set();
            }
        };

        _channel.BasicConsume(replyQueue.QueueName, true, consumer);
        mre.WaitOne(5000);
    
        return transactions;    
    }

    public void MAJTransaction(TransactionEntite p_transaction)
    {

        if (p_transaction == null){throw new ArgumentNullException("TransactionEntite p_transaction is null");}

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(p_transaction));

        _channel.BasicPublish(
            exchange:"",
            routingKey:"transactions",
            basicProperties:null,
            body:body);    }

    #endregion
}