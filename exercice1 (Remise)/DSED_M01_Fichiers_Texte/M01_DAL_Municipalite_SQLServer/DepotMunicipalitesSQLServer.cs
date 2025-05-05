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
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        if (p_Entite is null) { throw new ArgumentNullException(nameof(p_Entite)); }

        MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_Entite);
        this.m_dBContext.Add(municipaliteDTO);
        this.m_dBContext.SaveChanges();
        this.m_dBContext.ChangeTracker.Clear();
        p_Entite.CodeGeographique = municipaliteDTO.CodeGeographique;
    }

    public MunicipaliteEntite? ChercherMunicipaliteParCodeGeographique(int p_municipaliteCodeGeographique)
    {
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        if (p_municipaliteCodeGeographique < 1) { throw new ArgumentOutOfRangeException(nameof(p_municipaliteCodeGeographique)); }
        
        IQueryable<MunicipaliteDTO> requete = 
                                    this.m_dBContext.Municipalites 
                                    .Where(t => t.CodeGeographique == p_municipaliteCodeGeographique);
        
        return requete.Select(c => c.VerEntite()).SingleOrDefault();
    }

    public static bool Comparaison(MunicipaliteDTO p_Entite1)
    {
        return p_Entite1.Actif;
    }
    public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives()
    {
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        
        IQueryable<MunicipaliteDTO> requete = this.m_dBContext.Municipalites;
        return requete
            .Where(t => t.Actif )
            .Select(c => c.VerEntite())
            .ToList();
    }

    public void DesactiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBContext is null) {throw new ArgumentNullException(nameof(m_dBContext));}
        if (p_municipalite is null) {throw new ArgumentNullException(nameof(p_municipalite));}
        
        MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_municipalite);
        municipaliteDTO.Actif = false;
        this.m_dBContext.Municipalites.Update(municipaliteDTO);
        this.m_dBContext.SaveChanges();
        this.m_dBContext.ChangeTracker.Clear();
    }

    public void ActiverMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBContext is null) {throw new ArgumentNullException(nameof(m_dBContext));}
        if (p_municipalite is null) {throw new ArgumentNullException(nameof(p_municipalite));}
        
        MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_municipalite);
        municipaliteDTO.Actif = true;
        this.m_dBContext.Municipalites.Update(municipaliteDTO);
        this.m_dBContext.SaveChanges();
        this.m_dBContext.ChangeTracker.Clear();
    }

    public void MAJMunicipalite(MunicipaliteEntite p_municipalite)
    {
        if (m_dBContext is null) { throw new ArgumentNullException(nameof(m_dBContext)); }
        if (p_municipalite is null) { throw new ArgumentNullException(nameof(p_municipalite)); }
        
        // Trouver le DTO existant dans le contexte
        var municipaliteDTO = m_dBContext.Municipalites
            .FirstOrDefault(m => m.CodeGeographique == p_municipalite.CodeGeographique);

        if (municipaliteDTO is not null)
        {
            municipaliteDTO.CodeGeographique = p_municipalite.CodeGeographique;
            municipaliteDTO.NomMunicipalite = p_municipalite.NomMunicipalite;
            municipaliteDTO.AdresseWeb = p_municipalite.AdresseWeb;
            municipaliteDTO.AdresseCourriel = p_municipalite.AdresseCourriel;
            municipaliteDTO.DateProchaineElection = p_municipalite.DateProchaineElection;
            municipaliteDTO.Actif = true;
        }
        
        //MunicipaliteDTO municipaliteDTO = new MunicipaliteDTO(p_municipalite);
        this.m_dBContext.Update(municipaliteDTO);
        this.m_dBContext.SaveChanges();
        this.m_dBContext.ChangeTracker.Clear();
    }
    
  

    

    #endregion

 
}