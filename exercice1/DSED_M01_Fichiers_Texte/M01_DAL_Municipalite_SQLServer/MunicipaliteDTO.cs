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
    private Regex siteWebRegex = new Regex(@"^(https?:\/\/)?([\w\-]{1,50}\.)+[\w\-]{1,50}(\/[\w\-]{1,50})*$");
        
    #endregion
    
    #region Properties

    private int municipaliteId;
private string nomMunicipalite;
private string? adresseCourrielle;
private string? adresseWeb;
private DateTime dateProchaineElection;
private bool actif;

    [Key]
    public int MunicipaliteId
    {
        get { return this.municipaliteId; }
        set
        {
            if (value <= 0){throw new ArgumentOutOfRangeException("Numero  de municipalite doit être positi");} 
            else{this.municipaliteId = value;}
        }
    }

    public string NomMunicipalite
    {
        get{return this.nomMunicipalite;}
        set
        {
            if(value.Length < longueureNomMunicipalite){throw new ArgumentOutOfRangeException("Nom doit pas depace {longueureNomMunicipalite}");}
            else{this.nomMunicipalite = value;}
        }
    }

    public string? AdresseCourrielle
    {
        get{return this.adresseCourrielle;}
        set
        {
            if(!courrielRegex.IsMatch(value)){throw new AmbiguousMatchException("Le courriel ne respect pas le format attendu");}
            else
            {
                this.adresseCourrielle = value;
            }
        }
    }

    public string? AdresseWeb
    {
        get{return this.adresseWeb;}
        set
        {
            if(!siteWebRegex.IsMatch(value)){throw new AmbiguousMatchException("Le site web ne respect pas le format attendu");}
            else
            {
                this.adresseWeb = value;
            }
        }
    }

    public DateTime DateProchaineElection
    {
        get{return this.dateProchaineElection;}
        set
        {
            if(value <= DateTime.Today){throw new ArgumentOutOfRangeException("Date doit etre dans le futur");}
            else{this.dateProchaineElection = value;}
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
        
        MunicipaliteId = p_municipalite.CodeGeographique;
        NomMunicipalite = p_municipalite.NomMunicipalite;
        AdresseCourrielle = p_municipalite.AdresseCourrielle;
        AdresseWeb = p_municipalite.AdresseWeb;
        DateProchaineElection = DateTime.Now;
        Actif = true;
    }

    #endregion

    #region Methods

    public  MunicipaliteEntite VerEntite()
    {
        return new MunicipaliteEntite
        (this.MunicipaliteId
            , this.NomMunicipalite
            , this.AdresseCourrielle
            , this.AdresseWeb
            , this.DateProchaineElection);
    }

    #endregion
}