using M01_Entite;
using M01_Entite.IDepot;
using M01_DAL_Municipalite_SQLServer;

namespace M01_Srv_Municipalite;

public class TraitementImporterDonneesMunicipalite
{
    // objet input
    private readonly IDepotImportationMunicipalites depotImport;
    // obj poutput
    private readonly IDepotMunicipalites depotMunicipalite;

    private DepotMunicipalitesSQLServer depot;
    
    public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalites p_depotImportMunicipalites, IDepotMunicipalites p_depotMunicipalites) 
    {
        depotImport = p_depotImportMunicipalites;
        depotMunicipalite = p_depotMunicipalites;
    }

   public StatistiquesImportationDonnees Executer()
    {
        StatistiquesImportationDonnees stats = new StatistiquesImportationDonnees();

        /// Read CSV avec Impor Munic CSV
      IEnumerable<MunicipaliteEntite>listeEntree = depotImport.LireMunicipalites();
        stats.NombreEnregistrementsImportees += listeEntree.Count();
      
      MunicipaliteEntite municipaliteTest = depotMunicipalite.ChercherMunicipaliteParCodeGeographique(46005);
      
      
      foreach (MunicipaliteEntite ent in listeEntree)
      {
          //Query ent.id = bd.id 
          // if query ent.id is nul
          if (depotMunicipalite.ChercherMunicipaliteParCodeGeographique(ent.CodeGeographique) is null)
          {
              depotMunicipalite.AjouterMunicipalite(ent);
              stats.NombreEnregistrementsAjoutes++;
          }
          else if (!depotMunicipalite.ChercherMunicipaliteParCodeGeographique(ent.CodeGeographique).Equals(ent)
          {
              depotMunicipalite.MAJMunicipalite(ent);
              stats.NombreEnregistrementsModifies++;
              stats.NombreEnregistrementsNonModifies--;
          }
      }
      

      /// table importé besoin de comparer chaque enregistrement de l'inport avec son équivalent de la bd
        /// Besoin de fetcher la BD et comparer avec l'import, mais le traitement sera lourd
        
        return stats;
    }
    
}