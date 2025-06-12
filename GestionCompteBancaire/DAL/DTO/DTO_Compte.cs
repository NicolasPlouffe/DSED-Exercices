using Entite.IDepot;
namespace ClassLibrary1.DTO;
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

    public DTO_Compte(
        Guid p_numeroCompte, 
        TypeCompte p_typeCompte, 
        List<DTO_Transaction> p_listTransactions)
    {
        this.NumeroCompte = p_numeroCompte;
        this.TypeCompte = p_typeCompte;
        this.ListTransactions = p_listTransactions;
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
            listTransactions
        );
    }
    
    #endregion
}