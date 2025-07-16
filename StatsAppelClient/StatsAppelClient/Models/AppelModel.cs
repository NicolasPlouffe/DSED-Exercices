namespace StatsAppelClient.Models
{
    public class AppelModel
    {
        public int AppelId { get; set; }
        public int AgentId { get; set; }
        public DateTime PDebutAppel { get; set; }
        public DateTime? PFinAppel { get; set; }
        public TimeSpan? DureeAppel => PFinAppel.HasValue ? PFinAppel.Value - PDebutAppel : null;

        public AppelModel(DateTime p_debutAppel, DateTime p_finAppel)
        {
            this.PDebutAppel = p_debutAppel;
            this.PFinAppel = p_finAppel;
        }
        public AppelModel()
        {
            this.PDebutAppel = DateTime.Now;
        }
    }
}
