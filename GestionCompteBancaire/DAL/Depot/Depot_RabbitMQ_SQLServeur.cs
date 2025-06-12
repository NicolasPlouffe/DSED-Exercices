using Entite;

namespace DAL;
using Entite.IDepot;

public class Depot_RabbitMQ_SQLServeur:ICompteDepot,ITransactionDepot
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