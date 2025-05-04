using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;
using M01_Entite;

namespace M01_DAL_Municipalite_SQLServer;

public class MunicipaliteDTO
{
    #region variables et constantes

    private int longueureNomMunicipalite = 100;
    private int longueureCourrielWeb = 50;
    private Regex courrielRegex = new Regex(@"^[^@\s]{1,50}@[^@\s]{1,50}\.[^@\s]{1,50}$");
    private Regex siteWebRegex = new Regex(
        @"^(https?:\/\/)?" +                      // Protocole optionnel
        @"(www\.)?" +                              // Sous-domaine www optionnel
        @"([-a-zA-Z0-9@:%._\+~#=]{1,256}\.)" +     // Domaine principal
        @"[a-zA-Z0-9()]{1,6}" +                    // TLD (1-6 caractères)
        @"\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)$",     // Chemin/paramètres optionnels
        RegexOptions.Compiled
    );    
    #endregion
    
    #region Properties

    private int codeGeographique;
private string nomMunicipalite;
private string? adresseCourriel;
private string? adresseWeb;
private DateTime? dateProchaineElection;
private bool actif;

    [Key]
    public int CodeGeographique
    {
        get { return this.codeGeographique; }
        set
        {
            if (value <= 0){throw new ArgumentOutOfRangeException("Numero  de municipalite doit être positi");} 
            else{this.codeGeographique = value;}
        }
    }

    public string NomMunicipalite
    {
        get{return this.nomMunicipalite;}
        set
        {
            if(value.Length > longueureNomMunicipalite){throw new ArgumentOutOfRangeException("Nom doit pas depace {longueureNomMunicipalite}");}
            else{this.nomMunicipalite = value;}
        }
    }

    public string? AdresseCourriel
    {
        get{return this.adresseCourriel;}
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                this.adresseCourriel = null;
            }
            else
            {
                string valeurFiltree = value.Trim();

                if (!courrielRegex.IsMatch(valeurFiltree))
                {
                    throw new FormatException("Le courriel ne respecte pas le format attendu");
                }
                
                this.adresseCourriel = valeurFiltree;
            }
        }
    }

    public string? AdresseWeb
    {
        get{return this.adresseWeb;}
        set
        {
        if (string.IsNullOrWhiteSpace(value))
            {
                this.adresseWeb = null;
            }
            else
            {
                string valeurFiltree = value.Trim();

                if (!siteWebRegex.IsMatch(valeurFiltree))
                {
                    throw new FormatException("Le site web ne respect pas le format attendu");
                }
                this.adresseWeb = valeurFiltree;
                
            }
        }
    }

    public DateTime? DateProchaineElection
    {
        get{return this.dateProchaineElection;}
        set
        {
            //if(value <= DateTime.Today){throw new ArgumentOutOfRangeException("Date doit etre dans le futur");}
            //else
            {this.dateProchaineElection = value;}
        }
    }
    public bool Actif { get; set; }

    #endregion

    #region Constructor

    public MunicipaliteDTO()
    {
        ;
    }

    public MunicipaliteDTO(MunicipaliteEntite p_municipalite)
    {
        if(p_municipalite == null) {throw new ArgumentNullException(nameof(p_municipalite));}
        
        CodeGeographique = p_municipalite.CodeGeographique;
        NomMunicipalite = p_municipalite.NomMunicipalite;
        AdresseCourriel = p_municipalite.AdresseCourriel;
        AdresseWeb = p_municipalite.AdresseWeb;
        DateProchaineElection = p_municipalite.DateProchaineElection;
        Actif = true;
    }

    #endregion

    #region Methods

    public  MunicipaliteEntite VerEntite()
    {
        return new MunicipaliteEntite
        (this.CodeGeographique
            , this.NomMunicipalite
            , this.AdresseCourriel
            , this.AdresseWeb
            , this.DateProchaineElection ?? DateTime.Today.AddYears(5)
        );
    }

    #endregion
}