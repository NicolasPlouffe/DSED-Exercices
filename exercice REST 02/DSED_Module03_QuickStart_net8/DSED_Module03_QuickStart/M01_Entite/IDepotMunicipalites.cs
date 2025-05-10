namespace M01_Entite
{
    public interface IDepotMunicipalites
    {
        public MunicipaliteEntite? ChercherMunicipaliteParCodeGeographique(int p_codeGeographique);
        public IEnumerable<MunicipaliteEntite> ListerMunicipalitesActives();
        public void DesactiverMunicipalite(MunicipaliteEntite municipaliteEntite);
        public void AjouterMunicipalite(MunicipaliteEntite p_municipaliteEntite);
        public void MAJMunicipalite(MunicipaliteEntite municipaliteEntite);
    }
}
