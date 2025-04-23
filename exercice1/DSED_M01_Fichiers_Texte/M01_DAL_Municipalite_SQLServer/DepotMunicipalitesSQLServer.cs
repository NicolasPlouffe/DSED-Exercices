using M01_Entite;
using M01_Entite.IDepot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using M01_DAL_Municipalite_SQLServer;

namespace M01_DAL_Municipalite_SQLServer;

public class DepotMunicipalitesSQLServer : IDepotMunicipalites
{
    #region Attributs

    private readonly MunicipaliteContextSQLServer m_dBContext;
    
    #endregion

    #region Constructeur

    public DepotMunicipalitesSQLServer(MunicipaliteContextSQLServer p_context)
    {
        this.m_dBContext = p_context;
    }

    #endregion

    #region Methodes CRUD

    public void AjouterMunicipalite(MunicipaliteEntite p_Entite)
    {
        if (m_dBContext is null)
        {
            throw new ArgumentNullException(nameof(m_dBContext));
        }

        if (p_Entite is null)
        {
            throw new ArgumentNullException(nameof(p_Entite));
        }

        MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_Entite);
        this.m_dBContext.Add(municipaliteDTO);
        this.m_dBContext.SaveChanges();
        this.m_dBContext.ChangeTracker.Clear();
        p_Entite.CodeGeographique = municipaliteDTO.MunicipaliteId;
    }

    public MunicipaliteEntite? ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique)
    {
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        if (p_municipaliteCodeGeographique < 1) { throw new ArgumentOutOfRangeException(nameof(p_municipaliteCodeGeographique)); }
        
        IQueryable<MunicipaliteDTO> requete = this.m_dBContext.Municipalites.Where(t => t.MunicipaliteId == p_municipaliteCodeGeographique);
        return requete.Select(c => c.VerEntite()).SingleOrDefault();

    }

    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        
        IQueryable<MunicipaliteDTO> requete = this.m_dBContext.Municipalites;
        return requete.Select(o => o)
            .Where(t => t.Actif == true)
            .Select(c => c.VerEntite())
            .ToList();
    }

    public void DesactiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBContext is null) {throw new ArgumentNullException(nameof(m_dBContext));}
        if (p_municipalite is null) {throw new ArgumentNullException(nameof(p_municipalite));}
        
    }

    public void ActiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBContext is null) {throw new ArgumentNullException(nameof(m_dBContext));}
        if (p_municipalite is null) {throw new ArgumentNullException(nameof(p_municipalite));}
        
        
    }

    public void MAJMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        if (p_municipalite is null) { throw new ArgumentNullException(nameof(p_municipalite)); }
        
        MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_municipalite);
        this.m_dBContext.Update(municipaliteDTO);
        this.m_dBContext.SaveChanges();
        this.m_dBContext.ChangeTracker.Clear();
    }

    #endregion
}