using System;

using SRVM = M01_Entite;

namespace M01_DAL_Municipalite_SQLServer
{
    // DTO
    public class MunicipaliteDepot
    {
        public int MunicipaliteId { get; set; } = 0;
        public string NomMunicipalite { get; set; } = string.Empty;
        public string? AdresseCourriel { get; set; }
        public string? AdresseWeb { get; set; }
        public DateOnly? DateProchaineElection { get; set; } = DateOnly.FromDateTime(DateTime.Now);
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