using System.Globalization;
using M01_Entite;
using M01_Entite.IDepot;
using CsvHelper;

namespace M01_DAL_Import_Munic_CSV;

public class DepotImportationMunicipaliteCSV: IDepotImportationMunicipalites
{
    public string NomFichier { get; set; }
 
    public DepotImportationMunicipaliteCSV(string p_nomFichierAImporter)
    {
        NomFichier = p_nomFichierAImporter;
    }

    public IEnumerable<MunicipaliteEntite> LireMunicipalites()
    {
        using var reader = new StreamReader(NomFichier);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<MunicipaliteMap>();
        var records = csv.GetRecords<MunicipaliteEntite>().ToList();
        return records;
    }
}
// pour l'utilisation de CSV Helper https://wellsb.com/csharp/learn/read-csv-dotnet-csvhelper