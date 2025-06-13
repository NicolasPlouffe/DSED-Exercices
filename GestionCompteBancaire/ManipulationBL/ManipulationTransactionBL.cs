using Entite.IDepot;

namespace Entite.Manipulations;

public class ManipulationTransactionBL
{

    #region Attributes
    private readonly ITransactionDB _transactionDB;
    private readonly ITransactionDepot m_TransactionDepot;
    #endregion
    
    
    #region properties
    
    #endregion

    #region Constructors

    public ManipulationTransactionBL(ITransactionDB transactionDB, ITransactionDepot mTransactionDepot)
    {
        if (transactionDB is null) throw new ArgumentNullException(nameof(transactionDB));
        if (mTransactionDepot is null) throw new ArgumentNullException(nameof(mTransactionDepot));
        
        this._transactionDB = transactionDB;
        this.m_TransactionDepot = mTransactionDepot;
    }
    #endregion

    #region CRUD Methods
    // Creat Post
    public void AjouterTransaction(TransactionEntite p_transaction)
    {
        this.m_TransactionDepot.CreerTransaction(p_transaction);
    }
    
    //Read Get

    public TransactionEntite ObtenirTransaction(Guid p_id)
    {
        return this.m_TransactionDepot.LireTransaction(p_id);
    }
    
    //Uodate Put

    void ModifierTransaction(TransactionEntite p_Entite)
    {
        this.m_TransactionDepot.MAJTransaction(p_Entite);
    }
    
    #endregion
    
    #region methods
    
    #endregion

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}