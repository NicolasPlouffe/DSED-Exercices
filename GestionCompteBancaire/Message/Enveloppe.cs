namespace Message;

using Entite;

public class Enveloppe
{   public string Action { get; set; }
    public Guid ActionId { get; set; }
    public CompteEntite CompteEntite { get; set; }
    public TransactionEntite TransactionEntite { get; set; }

    public Enveloppe(
        string p_Action,
        CompteEntite? p_CompteEntite,
        TransactionEntite? p_TransactionEntite)
    {
        this.Action = p_Action;
        this.ActionId = Guid.NewGuid();
        if (p_CompteEntite is null && p_TransactionEntite is null)
            throw new ArgumentNullException("Au moins une entité doit être fournie");

        if (p_CompteEntite is not null && p_TransactionEntite is not null)
            throw new ArgumentException("Une seule entité doit être fournie");

        CompteEntite = p_CompteEntite;
        TransactionEntite = p_TransactionEntite;
    }
}