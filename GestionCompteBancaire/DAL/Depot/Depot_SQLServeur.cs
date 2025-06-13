using Entite;
using Entite.IDepot;

namespace DAL;

public class Depot_SQLServeur:ICompteDepot,ITransactionDepot
{
    #region Compte
    public void CreerCompte(CompteEntite p_compte)
    {
        throw new NotImplementedException();
    }

    public CompteEntite ObtenirCompte(Guid p_compteId)
    {
        throw new NotImplementedException();
    }

    public void MAJCompte(CompteEntite p_compte)
    {
        throw new NotImplementedException();
    }
    #endregion
    
    #region Transaction
    public void CreerTransaction()
    {
        throw new NotImplementedException();
    }

    public TransactionEntite LireTransaction()
    {
        throw new NotImplementedException();
    }

    public List<TransactionEntite> ListerToutesLesTransactions()
    {
        throw new NotImplementedException();
    }
    
    #endregion
}