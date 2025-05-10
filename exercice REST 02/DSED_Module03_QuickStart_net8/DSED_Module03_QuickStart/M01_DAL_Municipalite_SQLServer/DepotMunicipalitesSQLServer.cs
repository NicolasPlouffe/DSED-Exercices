using System;
using System.Collections.Generic;
using System.Linq;

using Entite = M01_Entite;

namespace M01_DAL_Municipalite_SQLServer
{
    public class DepotMunicipalitesSQLServer : Entite.IDepotMunicipalites
    {
        private MunicipaliteContextSQLServer m_contexte;

        public DepotMunicipalitesSQLServer(MunicipaliteContextSQLServer p_contexte)
        {
            if (p_contexte is null)
            {
                throw new ArgumentNullException(nameof(p_contexte));
            }

            this.m_contexte = p_contexte;
        }

        public void AjouterMunicipalite(Entite.MunicipaliteEntite p_municipaliteEntite)
        {
            if (p_municipaliteEntite is null)
            {
                throw new ArgumentNullException(nameof(p_municipaliteEntite));
            }

            if (this.m_contexte.Municipalite.Any(m => m.MunicipaliteId == p_municipaliteEntite.CodeGeographique))
            {
                MAJMunicipalite(p_municipaliteEntite);
            }
            else
            {
                this.m_contexte.Municipalite.Add(new MunicipaliteDepot(p_municipaliteEntite));
                this.m_contexte.SaveChanges();
            }
        }

        public Entite.MunicipaliteEntite? ChercherMunicipaliteParCodeGeographique(int p_codeGeographique)
        {
            return this.m_contexte.Municipalite.Where(m => m.MunicipaliteId == p_codeGeographique).Select(m => m.VersEntite()).SingleOrDefault();
        }

        public void DesactiverMunicipalite(Entite.MunicipaliteEntite municipaliteEntite)
        {
            if (municipaliteEntite is null)
            {
                throw new ArgumentNullException(nameof(municipaliteEntite));
            }

            MunicipaliteDepot? m = this.m_contexte.Municipalite.Where(m => m.MunicipaliteId == municipaliteEntite.CodeGeographique).SingleOrDefault();
            if (m is null)
            {
                throw new InvalidOperationException($"La municipalité d'identifiant {municipaliteEntite.CodeGeographique} n'existe pas dans le dépôt de données.");
            }

            m.Actif = false;
            this.m_contexte.Municipalite.Update(m);
            this.m_contexte.SaveChanges();
        }

        public IEnumerable<Entite.MunicipaliteEntite> ListerMunicipalitesActives()
        {
            return this.m_contexte.Municipalite.Where(m => m.Actif).Select(m => m.VersEntite()).ToList();
        }

        public void MAJMunicipalite(Entite.MunicipaliteEntite municipaliteEntite)
        {
            if (municipaliteEntite is null)
            {
                throw new ArgumentNullException(nameof(municipaliteEntite));
            }

            if (!this.m_contexte.Municipalite.Any(m => m.MunicipaliteId == municipaliteEntite.CodeGeographique))
            {
                throw new InvalidOperationException($"La municipalité d'identifiant {municipaliteEntite.CodeGeographique} n'existe pas dans le dépôt de données.");
            }

            MunicipaliteDepot municipaliteDepot = new MunicipaliteDepot(municipaliteEntite);
            this.m_contexte.Municipalite.Update(municipaliteDepot);
            this.m_contexte.SaveChanges();
        }
    }
}
