namespace M01_Srv_Municipalite;

public class StatistiquesImportationDonnees
{
    int NombreEnregistrementsAjoutes { get; set; }
    int NombreEnregistrementsModifies { get; set; }
    int NombreEnregistrementsDesactives { get; set; }
    int NombreEnregistrementsNonModifies { get; set; }
    int NombreEnregistrementsImportees { get; set; }
    

    public override string ToString()
    {
        return $"NombreEnregistrementsAjoutes: {NombreEnregistrementsAjoutes}, " +
               $"NombreEnregistrementsModifies: {NombreEnregistrementsModifies}, " +
               $"NombreEnregistrementsDesactives: {NombreEnregistrementsDesactives}, " +
               $"NombreEnregistrementsNonModifies: {NombreEnregistrementsNonModifies}, " +
               $"NombreEnregistrementsImportees: {NombreEnregistrementsImportees}";
    }
}