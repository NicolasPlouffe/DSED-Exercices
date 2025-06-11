namespace Entite;

public class TransactionEntite
{
  public Guid TransactionId { get; set; }
  public TypeTransaction Type { get; set; }
  public DateOnly DateTransaction { get; set; }
  public decimal Montant { get; set; }

  public TransactionEntite()
  {
    ;
  }
  
  public TransactionEntite(
    Guid transactionId,
    TypeTransaction p_type,
    DateOnly p_dateTransaction,
    decimal p_montant)
  {
    this.TransactionId = transactionId;
    this.Type = p_type;
    this.DateTransaction = p_dateTransaction;
    this.Montant = p_montant;
  }
  
  


}