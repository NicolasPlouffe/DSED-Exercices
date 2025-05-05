using System.Globalization;
using M01_Entite;
using M01_Entite.IDepot;
using M01_DAL_Municipalite_SQLServer;

namespace M01_Srv_Municipalite;

public class TraitementImporterDonneesMunicipalite
{
    // objet input
    private readonly IDepotImportationMunicipalites depotImport;
    // obj poutput
    private readonly IDepotMunicipalites depotMunicipalite ;
    
    public TraitementImporterDonneesMunicipalite(IDepotImportationMunicipalites p_depotImportMunicipalites, IDepotMunicipalites p_depotMunicipalites) 
    {
        depotImport = p_depotImportMunicipalites;
        depotMunicipalite = p_depotMunicipalites;
    }

   public StatistiquesImportationDonnees Executer()
    {
        StatistiquesImportationDonnees stats = new StatistiquesImportationDonnees();

        /// Read CSV avec Import Munic CSV
        IEnumerable<MunicipaliteEntite>listeEntiteCSV = depotImport.LireMunicipalites();
        stats.NombreEnregistrementsImportees += listeEntiteCSV.Count();
        
        // Liste des Municipalitée actives au niveau de la BD
        IEnumerable<MunicipaliteEntite> enregistrementsActifsBD = depotMunicipalite.ListerMunicipalitesActives();
        
        // Fruit d'une réflextion et collaboration avec le meilleur ruber duck de mon entourage
        // Jeff The Legend  Foxtrot, Bravo, Delta Tango 
        enregistrementsActifsBD.Where(m => !listeEntiteCSV.Contains(m)).ToList().ForEach(m => depotMunicipalite.DesactiverMunicipalite(m));
        
        // Ajout ou MAJ de la BD
        foreach (var enititeCSV in listeEntiteCSV)
        {
            MunicipaliteEntite entiteBD = depotMunicipalite.ChercherMunicipaliteParCodeGeographique(enititeCSV.CodeGeographique);
            
          if (entiteBD is null)
          {
              depotMunicipalite.AjouterMunicipalite(enititeCSV);
              stats.NombreEnregistrementsAjoutes++;
          }
      
          else if (!entiteBD.Equals(enititeCSV))
          {
              depotMunicipalite.MAJMunicipalite(enititeCSV);
              stats.NombreEnregistrementsModifies++;
          }
          else
          {
              stats.NombreEnregistrementsNonModifies++;
          }
        }
        return stats;
    }
}