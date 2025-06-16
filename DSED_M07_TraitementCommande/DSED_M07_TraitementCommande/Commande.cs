using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED_M07_TraitementCommande_producteur
{
    public class Commande
    {
        public Guid NoReferenceCommande { get; set; }
        public Type TypeEnvoie { get; set; }

        public string NomClient { get; set; }

        public List<Article> listArticles { get; set; }


        public Commande(Guid p_commandeId,Type p_type, string p_nomClient, List<Article> p_listeArticles)
        {
            this.NoReferenceCommande = p_commandeId;
            this.TypeEnvoie = p_type;
            this.NomClient = p_nomClient;
            this.listArticles = p_listeArticles;
        }
    }
}
