using Entite;
using Entite.IDepot;

namespace WebApplication1.Models;

public class CompteModel
{
    #region Properties
    
    public Guid NumeroCompte { get; set; }
    public TypeCompte TypeCompte { get; set; }
    public List<TransactionModel>? ListTransactions { get; set; }
    
    #endregion
    
    #region Constructor

    public CompteModel()
    {
        ;
    }
    
    public CompteModel(CompteEntite p_compte)
    {
        this.NumeroCompte = p_compte.NumeroCompte;
        this.TypeCompte = p_compte.TypeCompte;
        
        foreach (var transaction in p_compte.ListTransactions)
        {
            this.ListTransactions.Add(new TransactionModel(transaction));
        }
    }
    #endregion
    
    
    #region Methods

    public CompteEntite VerEntite()
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