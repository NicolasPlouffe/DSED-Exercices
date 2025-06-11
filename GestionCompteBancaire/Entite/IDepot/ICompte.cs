namespace Entite.IDepot;


public interface ICompte
{
    // Create
    void CreerCompte(CompteEntite p_compte);
    
    // Read
    CompteEntite AfficherCompte(Guid p_id_compte);
    
    // Update
    void MAJCompte(CompteEntite p_compte);
    
    //Delete
    void SupprimerCompte(Guid p_id_compte);
    
}