using Entite;
using Entite.IDepot;

namespace ClassLibrary1;

public class Depot_SQLServeur:ICompte,ITransaction
{
    #region Compte
    public void CreerCompte(CompteEntite p_compte)
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