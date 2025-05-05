namespace M01_Entite.IDepot;

public interface IDepotMunicipalites
{
    public void AjouterMunicipalite(MunicipaliteEntite p_Entite);
    MunicipaliteEntite ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique);
    IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives();
    void DesactiverMunicipalite(MunicipaliteEntite p_municipalite);
    void ActiverMunicipalite(MunicipaliteEntite p_municipalite);
    void MAJMunicipalite(MunicipaliteEntite p_municipalite);
}