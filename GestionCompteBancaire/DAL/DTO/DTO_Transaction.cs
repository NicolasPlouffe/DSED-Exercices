using System.Transactions;

namespace ClassLibrary1.DTO;
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

    public DTO_Transaction(
        Guid p_transactionId, 
        TypeTransaction p_type, 
        DateOnly p_dateTransaction, 
        decimal p_montant)
    {
        this.TransactionId = p_transactionId;
        this.Type = p_type;
        this.DateTransaction = p_dateTransaction;
        this.Montant = p_montant;
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