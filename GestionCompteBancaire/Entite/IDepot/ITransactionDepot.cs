namespace Entite.IDepot;

public interface ITransactionDepot
{
    // Create Post
    void CreerTransaction(TransactionEntite p_transaction);
    
      // Read Get
      TransactionEntite LireTransaction(Guid p_transactionId);
      List<TransactionEntite> ListerToutesLesTransactions();
      
      void MAJTransaction(TransactionEntite p_transaction);
    
}