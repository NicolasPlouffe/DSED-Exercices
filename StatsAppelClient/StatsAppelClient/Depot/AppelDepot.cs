using StatsAppelClient.Models;

namespace StatsAppelClient.Depot
{
    public class AppelDepot
    {
        public List<AppelModel> Appels { get; } = new List<AppelModel>();

        public AppelDepot()
        {
            ;
        }

        public int CalculerNbrAppelJourneeCourrante()
        {
            return this.Appels.Count(a => a.PDebutAppel.Date == DateTime.Today);
        }

        public double CalculerDureeMoyenneAppels()
        {
            var appelAvecDuree = this.Appels.Where(a => a.DureeAppel.HasValue);
            return appelAvecDuree.Any() ? appelAvecDuree.Average(d => d.DureeAppel.Value.TotalSeconds) : 0;
        }

        public int CalculerNbrAgentEnLigne()
        {
            return this.Appels.Count(a => !a.PFinAppel.HasValue);
        }
    }
}
