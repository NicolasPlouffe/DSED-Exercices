using System.Transactions;

namespace DAL.DTO;
using Entite;
public class DTO_Transaction
{
    #region Proprietes
    public Guid TransactionId { get; set; }
    public TypeTransaction Type { get; set; }
    public DateOnly DateTransaction { get; set; }
    public decimal Montant { get; set; }
    #endregion


    #region Constructeurs

    public DTO_Transaction()
    {
        ;
    }

    public DTO_Transaction(TransactionEntite p_transaction)
    {
        this.TransactionId = p_transaction.TransactionId;
        this.Type = p_transaction.Type;
        this.DateTransaction = p_transaction.DateTransaction;
        this.Montant = p_transaction.Montant;
    }
    #endregion

    #region Methodes

    public TransactionEntite VersEntite()
    {
        return new TransactionEntite(
            this.TransactionId,
            this.Type,
            this.DateTransaction,
            this.Montant
        );
    }
    
    #endregion
    
    
}