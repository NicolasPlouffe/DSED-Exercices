using Newtonsoft.Json;
using M01_Entite;
using M01_Entite.IDepot;

namespace M01_DAL_Import_Munic_JSON;

public class DepotImportationMunicipaliteJSON : IDepotImportationMunicipalites
{
    public string NomFichier { get; set; }

    public DepotImportationMunicipaliteJSON(string p_nomFichierAImporter)
    {
        NomFichier = p_nomFichierAImporter;
    }

    public IEnumerable<MunicipaliteEntite> LireMunicipalites()
    {
        try
        {
            using (StreamReader reader = new(NomFichier))
            {
                string json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<List<MunicipaliteEntite>>(json) ?? new List<MunicipaliteEntite>();
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur lors de la lecture du fichier JSON: {ex.Message}");
            throw new IOException($"Impossible de lire le fichier JSON {NomFichier}", ex);
        }
    }
}
