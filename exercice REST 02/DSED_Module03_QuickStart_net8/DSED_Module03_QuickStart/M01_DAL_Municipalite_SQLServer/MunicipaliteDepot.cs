using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SRVM = M01_Entite;

namespace M01_DAL_Municipalite_SQLServer
{
    [Table(("municipalites"))]
    public class MunicipaliteDepot
    {
        [Key]
        [Column("CodeGeographique")] 
        public int MunicipaliteId { get; set; } = 0;
        public string NomMunicipalite { get; set; } = string.Empty;
        public string? AdresseCourriel { get; set; }
        public string? AdresseWeb { get; set; }
        public DateTime? DateProchaineElection { get; set; } 
        public bool Actif { get; set; }

        public MunicipaliteDepot() { ; }

        public MunicipaliteDepot(SRVM.MunicipaliteEntite municipaliteEntite)
        {
            this.MunicipaliteId = municipaliteEntite.CodeGeographique;
            this.NomMunicipalite = municipaliteEntite.NomMunicipalite;
            this.AdresseCourriel = municipaliteEntite.AdresseCourriel;
            this.AdresseWeb = municipaliteEntite.AdresseWeb;
            this.DateProchaineElection = municipaliteEntite.DateProchaineElection;
            this.Actif = true;
        }

        public SRVM.MunicipaliteEntite VersEntite()
        {
            return new SRVM.MunicipaliteEntite(
                this.MunicipaliteId,
                this.NomMunicipalite,
                this.AdresseCourriel,
                this.AdresseWeb,
                this.DateProchaineElection 
            );
        }
    }
}