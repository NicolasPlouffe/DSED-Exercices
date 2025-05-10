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
        ;
    }

    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        return _depotMunicipalite.ListerMunicipalitesActives().Where(m => m.);
    }

    public void DesactiverMunicipalite(MunicipaliteEntite municipaliteEntite)
    {
        throw new NotImplementedException();
    }

    public void AjouterMunicipalite(MunicipaliteEntite municipaliteEntite)
    {
        throw new NotImplementedException();
    }

    public void MAJMunicipalite(MunicipaliteEntite municipaliteEntite)
    {
        throw new NotImplementedException();
    }
}