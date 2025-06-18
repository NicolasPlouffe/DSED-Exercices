using Entite.IDepot;

namespace Entite.Manipulations;

public class ManipulationTransactionBLConsoProd
{
    #region variables 
    #endregion
    
    #region Attributes
    private readonly ITransactionDepot _transactionDepotSQLServer; 
    private readonly ITransactionDepot _transactionDepotRabbitMQ; 
    #endregion
    
    #region constructors

    public ManipulationTransactionBLConsoProd
        (ITransactionDepot pTransactionDepotSqlServer
        ,ITransactionDepot pTransactionDepotRabbitMq)
    {
        ArgumentNullException.ThrowIfNull(pTransactionDepotSqlServer, nameof(pTransactionDepotSqlServer));
        ArgumentNullException.ThrowIfNull(pTransactionDepotRabbitMq, nameof(pTransactionDepotRabbitMq));
        
        this._transactionDepotSQLServer = pTransactionDepotSqlServer;
        this._transactionDepotRabbitMQ = pTransactionDepotRabbitMq;
    }
    #endregion
    
    #region CRUD Methods
    // CREATE Post
    public void CreerTransactionSQLServer(TransactionEntite p_transaction)
    {
        ArgumentNullException.ThrowIfNull(p_transaction, nameof(p_transaction));
        this._transactionDepotSQLServer.CreerTransaction(p_transaction);
    }
    
    // Read Get
    public void ObtenirTransactionSQLServer(Guid p_id)
    {
        this._transactionDepotSQLServer.LireTransaction(p_id);
    }   
    // Update Put

    public void ModifierTransactionSQLServer(TransactionEntite p_transaction)
    {
        ArgumentNullException.ThrowIfNull(p_transaction, nameof(p_transaction));
        this._transactionDepotSQLServer.MAJTransaction(p_transaction);
    }
    #endregion
    
    #region methods
    #endregion
}