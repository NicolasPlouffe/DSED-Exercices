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

        // Dicionnaire des codes Geo pour une comparaison rapide avec la BD, pour des raisons d'économie de traitement
        var dictionnaireCSV = listeEntiteCSV.ToDictionary(c => c.CodeGeographique, c => c);
        
        // Liste des Municipalitée actives au niveau de la BD
        IEnumerable<MunicipaliteEntite> enregistrementsActifsBD = depotMunicipalite.ListerMunicipalitesActives();
    
        // Construction du HAshSet pour codes GEO CSV
        var idsCSV = new HashSet<int>(dictionnaireCSV.Keys);
    
        //Mise a jour du status de la BD - pour désactiver s'il a eu des suppression de la source
        foreach (var codeGeoBD in enregistrementsActifsBD)
        {
            if (!idsCSV.Contains(codeGeoBD.CodeGeographique))
            {
                depotMunicipalite.DesactiverMunicipalite(codeGeoBD);
                stats.NombreEnregistrementsDesactives++;
            }
        }
    
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
              entiteBD.CodeGeographique = enititeCSV.CodeGeographique;
              entiteBD.NomMunicipalite = enititeCSV.NomMunicipalite;
              entiteBD.AdresseWeb = enititeCSV.AdresseWeb;
              entiteBD.AdresseCourrielle = enititeCSV.AdresseCourrielle;
              entiteBD.DateProchaineElection = enititeCSV.DateProchaineElection;
              
              depotMunicipalite.MAJMunicipalite(entiteBD);
              stats.NombreEnregistrementsModifies++;
              stats.NombreEnregistrementsNonModifies--;
          }
        }
        return stats;
    }
    
}