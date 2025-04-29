using System.Globalization;
using M01_Entite;
using M01_Entite.IDepot;
using CsvHelper;

namespace M01_DAL_Import_Munic_CSV;

public class DepotImportationMunicipaliteCSV: IDepotImportationMunicipalites
{
    private readonly string ligneEntete =
        "\"mcode\",\"munnom\",\"madr1\",\"madr2\",\"madr3\",\"madr4\",\"mcodpos\",\"mcourriel\",\"mweb\",\"mtel\",\"mfax\",\"mcodedesi\",\"mdes\",\"mgentile\",\"regadm\",\"divrec\",\"mrc\",\"admregionale\",\"horsmrcautoch\",\"mdatcons\",\"mdatregi\",\"msuperf\",\"mpopul\",\"datelec\",\"electype\",\"delectype\",\"elecmode\",\"delecmode\",\"divter\",\"mcm\",\"mcirc\",\"msupft\",\"maire\",\"con1\",\"con2\",\"con3\",\"con4\",\"con5\",\"con6\",\"con7\",\"con8\",\"con9\",\"con10\",\"con11\",\"con12\",\"con13\",\"con14\",\"con15\",\"con16\",\"con17\",\"con18\",\"con19\",\"con20\",\"con21\",\"con22\",\"con23\",\"con24\",\"con25\",\"con26\",\"con27\",\"con28\",\"con29\",\"con30\",\"con31\",\"con32\",\"con33\",\"con34\",\"con35\",\"con36\",\"con37\",\"con38\",\"con39\",\"con40\",\"con41\",\"con42\",\"con43\",\"con44\",\"con45\",\"con46\",\"con47\",\"con48\",\"con49\",\"con50\",\"con51\",\"con52\",\"con53\",\"con54\",\"con55\",\"con56\",\"con57\",\"con58\",\"con59\",\"con60\",\"con61\",\"con62\",\"con63\",\"con64\",\"con65\",\"con66\",\"con67\",\"con68\",\"con69\",\"con70\",\"con71\",\"con72\",\"con73\",\"con74\",\"con75\",\"dirgen\",\"dirsecpub\",\"tres\",\"gref\",\"sectres\",\"polic\",\"incen\",\"loisir\",\"trvpub\",\"mesurg\",\"urban\",\"communic\",\"permis\",\"batim\",\"nd\"";
    public string NomFichier { get; set; }
 
    public DepotImportationMunicipaliteCSV(string p_nomFichierAImporter)
    {
        NomFichier = p_nomFichierAImporter;
    }


    public IEnumerable<MunicipaliteEntite> LireMunicipalites()
    {
        List<MunicipaliteEntite> listeMunicipalite = new List<MunicipaliteEntite>();

        string ligneBrute;

        foreach (string ligne in File.ReadAllLines(NomFichier))
        {
            if (ligne == ligneEntete)
            {
                ;
            }
            
            var ligneDecoupe = ligne.Split("\",\"");
            if(int.TryParse(ligneDecoupe[0],out int code));
            if (DateTime.TryParse(ligneDecoupe[8], out DateTime date)) ;
  
            MunicipaliteEntite nouvelleMunicipalite = new MunicipaliteEntite(
                code
                ,ligneDecoupe[1]
                ,ligneDecoupe[7]
                ,ligneDecoupe[8]
                ,date);
            listeMunicipalite.Add(nouvelleMunicipalite);
        }
        
        return listeMunicipalite;
    }
}