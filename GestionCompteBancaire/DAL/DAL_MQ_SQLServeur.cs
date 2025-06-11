using Entite;

namespace ClassLibrary1;
using Entite.IDepot;

public class DAL_MQ_SQLServeur:ICompte,ITransaction
{
    public void CreerCompte(CompteEntite p_compte)
    {
        throw new NotImplementedException();
    }

    public CompteEntite AfficherCompte(Guid p_id_compte)
    {
        throw new NotImplementedException();
    }

    public void MAJCompte(CompteEntite p_compte)
    {
        throw new NotImplementedException();
    }

    public void SupprimerCompte(Guid p_id_compte)
    {
        throw new NotImplementedException();
    }

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

    public void MAJTransaction(TransactionEntite transaction)
    {
        throw new NotImplementedException();
    }

    public void SupprimerTransaction()
    {
        throw new NotImplementedException();
    }
}