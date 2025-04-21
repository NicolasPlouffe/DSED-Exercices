using M01_Entite;
using M01_Entite.IDepot;

namespace M01_DAL_Municipalite_SQLServer;

public class DepotMunicipalitesSQLServer:IDepotMunicipalites
{

    

    public DepotMunicipalitesSQLServer(MunicipaliteContextSQLServer p_context)
    {
        ;
    }
    
    public void AjouterMunicipalite()
    
    public IEnumerable<MunicipaliteEntite> ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        throw new NotImplementedException();
    }

    public void DesactiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        throw new NotImplementedException();
    }

    public void ActiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        throw new NotImplementedException();
    }

    public void MAJMunicipalite(MunicipaliteEntite p_municipalite)
    {
        throw new NotImplementedException();
    }
}