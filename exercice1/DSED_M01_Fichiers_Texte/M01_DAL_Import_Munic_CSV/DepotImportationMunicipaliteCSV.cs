using M01_Entite;
using M01_Entite.IDepot;

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
        throw new NotImplementedException();
    }
}