using Entite.IDepot;
namespace DAL.DTO;
using Entite;
public class DTO_Compte
{
    #region Proprietes
    public Guid NumeroCompte { get; set; }
    public TypeCompte TypeCompte { get; set; }
    public List<DTO_Transaction> ListTransactions { get; set; }
    #endregion
    
    #region Constructeurs

    public DTO_Compte()
    {
        ;
    }

    public DTO_Compte( CompteEntite p_compte)
    {
        this.NumeroCompte = p_compte.NumeroCompte;
        this.TypeCompte = p_compte.TypeCompte;
        foreach (var transaction in p_compte.ListTransactions)
        {
            this.ListTransactions.Add(new DTO_Transaction(transaction));
        }
    }
    
    #endregion

    #region Methodes

    public CompteEntite VersEntite()
    {
        List<TransactionEntite> listTransactions = new List<TransactionEntite>();

        foreach (var item in this.ListTransactions)
        {
            listTransactions.Add(item.VersEntite());
        }
        
        return new CompteEntite(
            this.NumeroCompte,
            this.TypeCompte,
            listTransactions);
    }
    
    #endregion
}