using System.Runtime.InteropServices.JavaScript;

namespace M01_Entite;

public class MunicipaliteEntite
{
    public int CodeGeographique { get; set; }
    public string NomMunicipalite { get; set; }
    public string? AdresseCourriel { get; set; }
    public string? AdresseWeb { get; set; }
    public DateTime? DateProchaineElection { get; set; }


    public MunicipaliteEntite(int codeGeographique, string nomMunicipalite, string? adresseCourriel, string? adresseWeb, DateTime dateProchaineElection)
    {
        CodeGeographique = codeGeographique;
        NomMunicipalite = nomMunicipalite;
        AdresseCourriel = adresseCourriel;
        AdresseWeb = adresseWeb;
        DateProchaineElection = dateProchaineElection;
    }

    public MunicipaliteEntite()
    {
        ;
    }
    
    public override bool Equals(object? obj)
    {
        return obj is MunicipaliteEntite municipalite &&
               CodeGeographique == municipalite.CodeGeographique && 
               NomMunicipalite == municipalite.NomMunicipalite &&
               AdresseCourriel == municipalite.AdresseCourriel &&
               AdresseWeb == municipalite.AdresseWeb &&
               DateProchaineElection == municipalite.DateProchaineElection;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            CodeGeographique, 
            NomMunicipalite, 
            AdresseCourriel, 
            AdresseWeb, 
            DateProchaineElection
        );
    }
}