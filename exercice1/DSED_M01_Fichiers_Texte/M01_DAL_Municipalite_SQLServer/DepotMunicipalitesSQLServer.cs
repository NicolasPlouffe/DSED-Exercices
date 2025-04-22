using M01_Entite;
using M01_Entite.IDepot;
using Microsoft.EntityFrameworkCore.SqlServer;

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
        if (m_dBConrtext is null)
        {
            throw new ArgumentNullException(nameof(m_dBConrtext));
        }

        if (p_Entite is null)
        {
            throw new ArgumentNullException(nameof(p_Entite));
        }

        MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_Entite);
        this.m_dBConrtext.Add(municipaliteDTO);
        this.m_dBConrtext.SaveChanges();
        this.m_dBConrtext.ChangeTracker.Clear();
        p_Entite.CodeGeographique = municipaliteDTO.MunicipaliteId;
    }

    public MunicipaliteEntite? ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique)
    {
        if (m_dBConrtext is null)
        {
            throw new ArgumentNullException(nameof(m_dBConrtext));
        }

        if (p_municipaliteCodeGeographique < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(p_municipaliteCodeGeographique));
        }

        IQueryable<MunicipaliteDTO> requete =
            this.m_dBConrtext.Municipalites.Where(c => c.CodeGeographique == p_municipaliteCodeGeographique);
        return requete.Select(c => c.VerEntite()).SingleOrDefault();
    }

    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        if (m_dBConrtext is null)
        {
            throw new ArgumentNullException(nameof(m_dBConrtext));
        }
        IQueryable<MunicipaliteDTO> requete = this.m_dBConrtext.Municipalites;
        return requete.Select(c => c.VerEntite()).ToList();
    }

    public void DesactiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBConrtext is null)
        {
            throw new ArgumentNullException(nameof(m_dBConrtext));
        }
    }

    public void ActiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBConrtext is null)
        {
            throw new ArgumentNullException(nameof(m_dBConrtext));
        }
    }

    public void MAJMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBConrtext is null)
        {
            throw new ArgumentNullException(nameof(m_dBConrtext));
        }
    }

    #endregion
}