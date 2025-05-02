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
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return GetType().Name.GetHashCode();
    }
}