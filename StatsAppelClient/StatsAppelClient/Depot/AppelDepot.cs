using StatsAppelClient.Models;

namespace StatsAppelClient.Depot
{
    public class AppelDepot
    {
        public List<AppelModel> Appels { get; } = new List<AppelModel>();
        
        public AppelDepot()
        {
            InitialiserDesDonnesTests();
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

        private void InitialiserDesDonnesTests()
        {
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(8),
                PFinAppel = DateTime.Today.AddHours(8).AddMinutes(30),
            });
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(9),
                PFinAppel = DateTime.Today.AddHours(9).AddMinutes(45),
            });
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(10),
                PFinAppel = null,
            });
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(11),
                PFinAppel = DateTime.Today.AddHours(11).AddMinutes(15),
            });
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(12),
                PFinAppel = null,
            });
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(13),
                PFinAppel = DateTime.Today.AddHours(13).AddMinutes(50),
            });
            Appels.Add(new AppelModel
            {
                PDebutAppel = DateTime.Today.AddHours(14),
                PFinAppel = null,
            });
        }
    }
}
