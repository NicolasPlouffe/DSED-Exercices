namespace Entite;

public class TransactionEntite
{
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
        TransactionId = transactionId;
        Type = p_type;
        DateTransaction = p_dateTransaction;
        Montant = p_montant;
    }

    public Guid TransactionId { get; set; }
    public TypeTransaction Type { get; set; }
    public DateOnly DateTransaction { get; set; }
    public decimal Montant { get; set; }

    #region methods

    public override string ToString()
    {
        return $"Le numero de la transaction est : {TransactionId} \n" +
               $"Le type de transaction est : {Type} \n" +
               $"La date de la transaction est : {DateTransaction} \n" +
               $"Le montant de la transaction est : {Montant} \n";
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as TransactionEntite);
    }
    
    #endregion
}