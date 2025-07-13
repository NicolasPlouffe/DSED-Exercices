namespace Module08_StatsServiceClient.Models;

public class Appel
{
    public int AppelId { get; set; }
    public int AgentId { get; set; }
    public DateTime DebutAppel { get; set; }
    public DateTime? FinAppel { get; set; }
    public TimeSpan? DureeAppel => FinAppel.HasValue ? FinAppel.Value - DebutAppel : null;

    public Appel()
    {
        
        this.DebutAppel = DateTime.Now;
    }
}