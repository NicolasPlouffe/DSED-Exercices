using M01_Entite;
namespace M03_Srv_Municipalite;

public class ManipulationMunicipalites:IDepotMunicipalites
{
    private readonly IDepotMunicipalites _depotMunicipalite;


    public ManipulationMunicipalites(IDepotMunicipalites p_municipalite)
    {
        this._depotMunicipalite = p_municipalite;
    }
    
    
    public MunicipaliteEntite? ChercherMunicipaliteParCodeGeographique(int p_codeGeographique)
    {
        MunicipaliteEntite? e_municipalite = _depotMunicipalite.ChercherMunicipaliteParCodeGeographique(p_codeGeographique);
        return e_municipalite?.Actif == false ? e_municipalite : null;
    }

    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        return _depotMunicipalite.ListerMunicipalitesActives().Where(m => !m.Actif);
    }

    public void DesactiverMunicipalite(MunicipaliteEntite p_municipaliteEntite)
    {
        p_municipaliteEntite.Actif = false;
        _depotMunicipalite.DesactiverMunicipalite(p_municipaliteEntite);
    }

    public void AjouterMunicipalite(MunicipaliteEntite p_municipaliteEntite)
    { 
        if (p_municipaliteEntite is null) { throw new ArgumentNullException("La municipalite passee en param ne peut etre null"); }
        
        _depotMunicipalite.AjouterMunicipalite(p_municipaliteEntite);
    }

    public void MAJMunicipalite(MunicipaliteEntite p_municipaliteEntite)
    {
        if (p_municipaliteEntite is null) { throw new ArgumentNullException("La municipalite passee en param ne peut etre null"); }
        _depotMunicipalite.MAJMunicipalite(p_municipaliteEntite);
    }
}