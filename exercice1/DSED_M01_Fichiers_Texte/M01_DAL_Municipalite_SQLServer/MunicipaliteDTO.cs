using M01_Entite;

namespace M01_DAL_Municipalite_SQLServer;

public class MunicipaliteDTO
{
    public int MunicipaliteId { get; set; }
    public string NomMunicipalite { get; set; }
    public string? AdresseCourrielle { get; set; }
    public string? AdresseWeb { get; set; }
    public DateTime DateProchaineElection { get; set; }
    public bool Actif { get; set; }

    public MunicipaliteDTO()
    {
        ;
    }

   
}