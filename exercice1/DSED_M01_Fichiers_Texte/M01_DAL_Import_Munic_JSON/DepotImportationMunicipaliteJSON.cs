using M01_Entite;
using M01_Entite.IDepot;

namespace M01_DAL_Import_Munic_JSON;

public class DepotImportationMunicipaliteJSON:IDepotImportationMunicipalites
{
    public string NomFichier { get; set; }
    
    public DepotImportationMunicipaliteJSON(string p_nomFichierAImporter)
    {
        NomFichier = p_nomFichierAImporter;
    }

    public IEnumerable<MunicipaliteEntite> LireMunicipalites()
    {
        throw new NotImplementedException();
    }
}
