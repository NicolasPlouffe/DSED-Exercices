namespace M01_Entite.IDepot;

public interface IDepotMunicipalites
{
    MunicipaliteEntite ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique);
    IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives();
    void DesactiverMunicipalite(MunicipaliteEntite p_municipalite);
    void ActiverMunicipalite(MunicipaliteEntite p_municipalite);
    void MAJMunicipalite(MunicipaliteEntite p_municipalite);
}