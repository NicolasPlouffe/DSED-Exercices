namespace M01_Entite;

public interface IDepotClefAPI
{
    public ClefAPIEntite? ChercherClefAPI();
    public void AjouterClefAPI(ClefAPIEntite p_clefAPIEntite);
    public void ModifierClefAPI(ClefAPIEntite p_clefAPIEntite);
  
}