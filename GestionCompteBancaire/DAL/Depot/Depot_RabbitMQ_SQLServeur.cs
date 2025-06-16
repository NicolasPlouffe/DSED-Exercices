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
    public void CreerTransaction(TransactionEntite p_transaction)
    {
        throw new NotImplementedException();
    }

    public TransactionEntite LireTransaction(Guid p_transactionId)
    {
        throw new NotImplementedException();
    }

    public List<TransactionEntite> ListerToutesLesTransactions()
    {
        throw new NotImplementedException();
    }

    public void MAJTransaction(TransactionEntite p_transaction)
    {
        throw new NotImplementedException();
    }

    #endregion
}