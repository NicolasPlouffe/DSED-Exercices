using System.Text;
using Entite.IDepot;

namespace Entite;

#region Properties

public class CompteEntite
{
    public Guid NumeroCompte { get; set; }
    public TypeCompte TypeCompte { get; set; }
    public List<TransactionEntite> ListTransactions { get; set; }

    #endregion

    
    #region Constructor

    public CompteEntite()
    {
        NumeroCompte = Guid.NewGuid();
        TypeCompte = TypeCompte.Courrant;
    }

    public CompteEntite(
        Guid p_numeroCompte,
        TypeCompte p_typeCompte,
        List<TransactionEntite> p_listTransactions)
    {
        NumeroCompte = p_numeroCompte;
        TypeCompte = p_typeCompte;
        ListTransactions = p_listTransactions;
    }
    #endregion
    
    #region Methods

    public override string ToString()
    {
        var transactionListBuilder = new StringBuilder();
        foreach (var p_transaction in ListTransactions) transactionListBuilder.AppendLine(p_transaction.ToString());
        var toReturn = $"Le numero de compte est {NumeroCompte} \n" +
                       $"Le type de compte est {TypeCompte} \n" +
                       $"La liste de transactions est : \n{transactionListBuilder}";

        return toReturn;
    }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as CompteEntite);
    }

    #endregion
}