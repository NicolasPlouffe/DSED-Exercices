namespace M01_Entite
{
    public class MunicipaliteEntite
    {
        public int CodeGeographique { get; private set; }
        public string NomMunicipalite { get; private set; }
        public string? AdresseCourriel { get; private set; }
        public string? AdresseWeb { get; private set; }
        public DateOnly? DateProchaineElection { get; private set; }

        public bool Actif { get; set; } 

        public MunicipaliteEntite(int p_codeGeographique, string p_nomMunicipalite, string? p_adresseCourriel, string? p_adresseWeb, DateOnly? p_dateProchaineElection)
        {
            this.CodeGeographique = p_codeGeographique;
            this.NomMunicipalite = p_nomMunicipalite;
            this.AdresseCourriel = p_adresseCourriel;
            this.AdresseWeb = p_adresseWeb;
            this.DateProchaineElection = p_dateProchaineElection;
            }

        public override bool Equals(object? obj)
        {
            MunicipaliteEntite? objAComparer = obj as MunicipaliteEntite;

            return objAComparer is not null
                   && this.CodeGeographique == objAComparer.CodeGeographique
                   && string.Compare(this.NomMunicipalite, objAComparer.NomMunicipalite) == 0
                   && string.Compare(this.AdresseCourriel, objAComparer.AdresseCourriel) == 0
                   && string.Compare(this.AdresseWeb, objAComparer.AdresseWeb) == 0
                   && this.DateProchaineElection == objAComparer.DateProchaineElection
                   ;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CodeGeographique, NomMunicipalite, AdresseCourriel, AdresseWeb, DateProchaineElection);
        }

        public static bool operator ==(MunicipaliteEntite left, MunicipaliteEntite right)
        {
            return EqualityComparer<MunicipaliteEntite>.Default.Equals(left, right);
        }

        public static bool operator !=(MunicipaliteEntite left, MunicipaliteEntite right)
        {
            return !(left == right);
        }
    }
}