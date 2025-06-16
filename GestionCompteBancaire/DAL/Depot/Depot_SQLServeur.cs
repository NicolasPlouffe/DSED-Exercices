using DAL;
using DAL.DTO;
using Entite;
using Entite.IDepot;

namespace DAL;

public class Depot_SQLServeur:ICompteDepot,ITransactionDepot
{
    #region Attributs

    private readonly ApplicationDBContexte _dbContexte;
    #endregion
    
    
    #region Constructeurs

    public Depot_SQLServeur(ApplicationDBContexte p_dbContexte)
    {
        this._dbContexte = p_dbContexte;
    }
    #endregion
    
    
    #region Compte
    public void CreerCompte(CompteEntite p_compte)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
        if(p_compte is null){throw new ArgumentNullException(nameof(p_compte));}
        
        DTO_Compte nouveauCompte = new DTO_Compte(p_compte);
        this._dbContexte.
        
    }

    public CompteEntite ObtenirCompte(Guid p_compteId)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
    }

    public void MAJCompte(CompteEntite p_compte)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
    }
    #endregion
    
    #region Transaction
    public void CreerTransaction(TransactionEntite p_transaction)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
    }

    public TransactionEntite LireTransaction(Guid p_transactionId)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
    }

    public List<TransactionEntite> ListerToutesLesTransactions()
    {
        throw new NotImplementedException();
    }

    public void MAJTransaction(TransactionEntite p_transaction)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
    }

    #endregion
}