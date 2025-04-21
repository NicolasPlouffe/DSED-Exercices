using M01_Entite;
using M01_Entite.IDepot;

namespace M01_DAL_Municipalite_SQLServer;

public class DepotMunicipalitesSQLServer : IDepotMunicipalites
{
    #region Attributs

    private readonly MunicipaliteContextSQLServer m_dBConrtext;

    #endregion

    #region Constructeur

    public DepotMunicipalitesSQLServer(MunicipaliteContextSQLServer p_context)
    {
        this.m_dBConrtext = p_context;
    }

    #endregion

    #region Methodes CRUD

    public void AjouterMunicipalite(MunicipaliteEntite p_Entite)
    {
        if (m_dBConrtext is null) { throw new ArgumentNullException(nameof(m_dBConrtext)); }
        if (p_Entite is null) { throw new ArgumentNullException(nameof(p_Entite)); }
    }

    public IEnumerable<MunicipaliteEntite> ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique)
    {
        if (m_dBConrtext is null) { throw new ArgumentNullException(nameof(m_dBConrtext)); }
    }

    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        if (m_dBConrtext is null) { throw new ArgumentNullException(nameof(m_dBConrtext)); }
    }

    public void DesactiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBConrtext is null) { throw new ArgumentNullException(nameof(m_dBConrtext)); }
    }

    public void ActiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBConrtext is null) { throw new ArgumentNullException(nameof(m_dBConrtext)); }
    }

    public void MAJMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBConrtext is null) { throw new ArgumentNullException(nameof(m_dBConrtext)); }
    }

    #endregion
}