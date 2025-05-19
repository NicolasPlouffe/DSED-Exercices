using M01_Entite;
namespace M03_Web_Municipalites_REST01.Models;

public class MunicipaliteModel
{
    public int MunicipaliteId { get; set; } = 0;
    public string NomMunicipalite { get; set; } = string.Empty;
    public string? AdresseCourriel { get; set; }
    public string? AdresseWeb { get; set; }
    public DateTime? DateProchaineElection { get; set; } = DateTime.Now;
    public bool Actif { get; set; }

    public MunicipaliteModel()
    {
        ;
    }

    public MunicipaliteModel(MunicipaliteEntite municipaliteEntite)
    {
        this.MunicipaliteId = municipaliteEntite.CodeGeographique;
        this.NomMunicipalite = municipaliteEntite.NomMunicipalite;
        this.AdresseCourriel = municipaliteEntite.AdresseCourriel;
        this.AdresseWeb = municipaliteEntite.AdresseWeb;
        this.DateProchaineElection = municipaliteEntite.DateProchaineElection;
        this.Actif = true;
    }

    public MunicipaliteEntite VersEntite()
    {
        return new MunicipaliteEntite(
            this.MunicipaliteId,
            this.NomMunicipalite,
            this.AdresseCourriel,
            this.AdresseWeb,
            this.DateProchaineElection
        );
    }
}