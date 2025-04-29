using CsvHelper.Configuration;
using M01_Entite;

namespace M01_DAL_Import_Munic_CSV;

public sealed class MunicipaliteMap : ClassMap<MunicipaliteEntite>
{
    public MunicipaliteMap()
    {
        Map(m => m.CodeGeographique).Name("mcode");
        Map(m => m.NomMunicipalite).Name("munnom");
        Map(m => m.AdresseCourrielle).Name("mcourriel");
        Map(m => m.AdresseWeb).Name("mweb");
        Map(m => m.DateProchaineElection).Name("datelec");
    }
}