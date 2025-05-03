using System.Runtime.InteropServices.JavaScript;

namespace M01_Entite;

public class MunicipaliteEntite
{
    public int CodeGeographique { get; set; }
    public string NomMunicipalite { get; set; }
    public string? AdresseCourrielle { get; set; }
    public string? AdresseWeb { get; set; }
    public DateTime? DateProchaineElection { get; set; }


    public MunicipaliteEntite(int codeGeographique, string nomMunicipalite, string? adresseCourrielle, string? adresseWeb, DateTime dateProchaineElection)
    {
        CodeGeographique = codeGeographique;
        NomMunicipalite = nomMunicipalite;
        AdresseCourrielle = adresseCourrielle;
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
               AdresseCourrielle == municipalite.AdresseCourrielle &&
               AdresseWeb == municipalite.AdresseWeb &&
               DateProchaineElection == municipalite.DateProchaineElection;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(
            CodeGeographique, 
            NomMunicipalite, 
            AdresseCourrielle, 
            AdresseWeb, 
            DateProchaineElection
        );
    }
}