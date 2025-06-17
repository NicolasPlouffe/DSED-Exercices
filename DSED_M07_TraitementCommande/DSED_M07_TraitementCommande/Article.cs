using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED_M07_TraitementCommande_producteur
{
    public class Article
    {
        public Guid NoReference { get; set; }
        public string Nom { get; set; }
        public decimal Prix { get; set; }
        public int Quantite { get; set; }


        public Article(Guid p_id, string p_nom, decimal p_prix, int p_qte)
        {
            this.NoReference = p_id;
            this.Nom = p_nom;
            this.Prix = p_prix;
            this.Quantite = p_qte;
        }

        public override string ToString()
        {
            string toReturn = $"" +
               $"Numero reference {this.NoReference}" +
               $"Nom : {this.Nom}" +
               $"Prix : {this.Prix}" +
               $"Quantite : {this.Quantite}";

            return toReturn;
        }
    }
}
