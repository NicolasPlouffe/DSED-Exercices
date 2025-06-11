using Entite.IDepot;

namespace Entite;

public class CompteEntite
{
 public Guid NumeroCompte { get; set; }
 public TypeCompte TypeCompte { get; set; }
 public List<TransactionEntite> ListTransactions { get; set; }

 public CompteEntite()
 {
  this.NumeroCompte = Guid.NewGuid();
  this.TypeCompte = TypeCompte.Courrant;
 }
 
 public CompteEntite(
  Guid p_numeroCompte,
  TypeCompte p_typeCompte,
  List<TransactionEntite> p_listTransactions)
 {
  this.NumeroCompte = p_numeroCompte;
  this.TypeCompte = p_typeCompte;
  this.ListTransactions = p_listTransactions;
 }
 
}