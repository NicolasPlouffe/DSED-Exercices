namespace M01_Srv_Municipalite;

public class StatistiquesImportationDonnees
{
    public int NombreEnregistrementsAjoutes { get; set; }
    public int NombreEnregistrementsModifies { get; set; }
    public int NombreEnregistrementsDesactives { get; set; }
    public int NombreEnregistrementsNonModifies { get; set; }
    public int NombreEnregistrementsImportees { get; set; }
    

    public override string ToString()
    {
        return $"NombreEnregistrementsAjoutes: {NombreEnregistrementsAjoutes}, " +
               $"NombreEnregistrementsModifies: {NombreEnregistrementsModifies}, " +
               $"NombreEnregistrementsDesactives: {NombreEnregistrementsDesactives}, " +
               $"NombreEnregistrementsNonModifies: {NombreEnregistrementsNonModifies}, " +
               $"NombreEnregistrementsImportees: {NombreEnregistrementsImportees}";
    }
}