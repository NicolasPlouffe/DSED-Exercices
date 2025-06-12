namespace Entite.IDepot;

public interface ITransactionDepot
{
    // Create Post
    void CreerTransaction();
    
      // Read Get
      TransactionEntite LireTransaction();
      List<TransactionEntite> ListerToutesLesTransactions();
    
}