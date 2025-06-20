namespace Entite.IDepot;


public interface ICompteDepot
{
    // Create Post
    void CreerCompte(CompteEntite p_compte);
    
    // Read Get
    CompteEntite ObtenirCompte(Guid p_compteId);
    // Updat Put 
    void MAJCompte(CompteEntite p_compte);
    
 
    
}