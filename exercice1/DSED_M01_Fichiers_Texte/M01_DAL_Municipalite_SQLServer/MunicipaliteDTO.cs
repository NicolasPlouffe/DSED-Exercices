using System.ComponentModel.DataAnnotations;
using M01_Entite;

namespace M01_DAL_Municipalite_SQLServer;

public class MunicipaliteDTO
{
    #region Properties

    [Key] public int MunicipaliteId { get; set; }
    public string NomMunicipalite { get; set; }
    public string? AdresseCourrielle { get; set; }
    public string? AdresseWeb { get; set; }
    public DateTime DateProchaineElection { get; set; }
    public bool Actif { get; set; }

    #endregion

    #region Constructor

    /*public MunicipaliteDTO()
    {
        ;
    }*/

    public MunicipaliteDTO(MunicipaliteEntite p_municipalite)
    {
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