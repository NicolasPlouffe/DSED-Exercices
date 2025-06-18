using Entite.IDepot;

namespace Entite.Manipulations;

public class ManipulationTransactionBLProd
{

    #region Attributes
    private readonly ITransactionDepot _mTransactionDepotSQLServer; 
    private readonly ITransactionDepot _mTransactionDepotRabbit; 
    #endregion
    
    
    #region properties
    
    #endregion

    #region Constructors

    public ManipulationTransactionBLProd(ITransactionDepot p_TransactionDepotSQLServer, ITransactionDepot p_TransactionDepotRabbit)
    {
        if (p_TransactionDepotSQLServer is null) throw new ArgumentNullException(nameof(p_TransactionDepotSQLServer));
        if (p_TransactionDepotRabbit is null) throw new ArgumentNullException(nameof(p_TransactionDepotRabbit));

        this._mTransactionDepotSQLServer = p_TransactionDepotSQLServer;
        this._mTransactionDepotRabbit = p_TransactionDepotRabbit;
    }
    #endregion

    #region CRUD Methods
    // Creat Post
    public void AjouterTransaction(TransactionEntite p_transaction)
    {
        this._mTransactionDepotRabbit.CreerTransaction(p_transaction);
    }
    
    //Read Get

    public TransactionEntite ObtenirTransaction(Guid p_id)
    {
        return this._mTransactionDepotSQLServer.LireTransaction(p_id);

    }
    
    //Uodate Put

    public void ModifierTransaction(TransactionEntite p_Entite)
    {
        this._mTransactionDepotRabbit.MAJTransaction(p_Entite);
    }
    
    #endregion
    
    #region methods
    
    #endregion

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}