using M01_Entite;
namespace M03_Web_Municipalites_REST01.Models;

public class MunicipaliteModel
{
    public int MunicipaliteId { get; set; } = 0;
    public string NomMunicipalite { get; set; } = string.Empty;
    public string? AdresseCourriel { get; set; }
    public string? AdresseWeb { get; set; }
    public DateOnly? DateProchaineElection { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public bool Actif { get; set; }

    public MunicipaliteModel()
    {
        ;
    }

    public MunicipaliteModel(Municipalite p_municipalite)
    {
        this.MunicipaliteId = p_municipalite.CodeGeographique;
        this.NomMunicipalite = p_municipalite.NomMunicipalite;
        this.AdresseCourriel = p_municipalite.AdresseCourriel;
        this.AdresseWeb = p_municipalite.AdresseWeb;
        this.DateProchaineElection = p_municipalite.DateProchaineElection;
        this.Actif = true;
    }

    public Municipalite VersEntite()
    {
        return new Municipalite(
            this.MunicipaliteId,
            this.NomMunicipalite,
            this.AdresseCourriel,
            this.AdresseWeb,
            this.DateProchaineElection
        );
    }
}