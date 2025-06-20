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
        this._dbContexte.Add(nouveauCompte);
        this._dbContexte.SaveChanges();
        this._dbContexte.ChangeTracker.Clear();
        p_compte.NumeroCompte = nouveauCompte.NumeroCompte;
        
    }

    public CompteEntite? ObtenirCompte(Guid p_compteId)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
        
        IQueryable<DTO_Compte> requete = this._dbContexte.Comptes.Where(c => c.NumeroCompte == p_compteId);
        return requete.Select(c=> c.VersEntite()).SingleOrDefault();
    }

    public void MAJCompte(CompteEntite p_compte)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
        if (p_compte is null){throw new ArgumentNullException(nameof(p_compte));}
        
        DTO_Compte nouveau = new DTO_Compte(p_compte);
        this._dbContexte.Update(nouveau);
        this._dbContexte.SaveChanges();
        this._dbContexte.ChangeTracker.Clear();
    }
    #endregion
    
    #region Transaction
    public void CreerTransaction(TransactionEntite p_transaction)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
        if (p_transaction is null){throw new ArgumentNullException(nameof(p_transaction));}
        
        DTO_Transaction nouveau = new DTO_Transaction(p_transaction);
        this._dbContexte.Add(nouveau);
        this._dbContexte.SaveChanges();
        this._dbContexte.ChangeTracker.Clear();
        p_transaction.TransactionId = nouveau.TransactionId;
    }

    public TransactionEntite LireTransaction(Guid p_transactionId)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
        
        IQueryable<DTO_Transaction> requete = this._dbContexte.Transactions.Where(c => c.TransactionId == p_transactionId);
        return requete.Select(c=> c.VersEntite()).SingleOrDefault();
    }

    public List<TransactionEntite> ListerToutesLesTransactions()
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}

        IQueryable<DTO_Transaction> requete = this._dbContexte.Transactions;
        return requete.Select(c=> c.VersEntite()).ToList();
     
    }

    public void MAJTransaction(TransactionEntite p_transaction)
    {
        if (_dbContexte is null){throw new ArgumentNullException(nameof(_dbContexte));}
        if (p_transaction is null){throw new ArgumentNullException(nameof(p_transaction));}
        
        DTO_Transaction nouveau = new DTO_Transaction(p_transaction);
        this._dbContexte.Update(nouveau);
        this._dbContexte.SaveChanges();
        this._dbContexte.ChangeTracker.Clear();
    }

    #endregion
}