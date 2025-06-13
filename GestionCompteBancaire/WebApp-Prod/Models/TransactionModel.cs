using System.Transactions;
using Entite;
namespace WebApplication1.Models;

public class TransactionModel
{
    #region Properties
    
    public Guid TransactionId { get; set; }
    public TypeTransaction Type { get; set; }
    public DateOnly DateTransaction { get; set; }
    public decimal Montant { get; set; }
    
    #endregion
    
    #region Constructor

    public TransactionModel()
    {
        ;
    }
    public TransactionModel(TransactionEntite p_transaction)
    {
        this.TransactionId = p_transaction.TransactionId;
        this.Type = p_transaction.Type;
        this.DateTransaction = p_transaction.DateTransaction;
        this.Montant = p_transaction.Montant;
    }
    #endregion
    
    
    #region Methods

    public TransactionEntite VersEntite()
    {
        return new TransactionEntite(
            this.TransactionId,
            this.Type,
            this.DateTransaction,
            this.Montant);
    }
    
    #endregion
}