namespace Entite.IDepot;

public interface ITransaction
{
    // Create Post
    void CreerTransaction();
    
      // Read Get
      TransactionEntite LireTransaction();
        List<TransactionEntite> ListerToutesLesTransactions();
      
      // Update
    void MAJTransaction(TransactionEntite transaction);
    
    //Delete
    void SupprimerTransaction();
    
}