namespace StatsAppelClient.Models;

public class StatsAppelModel
{
    public int NbrAppelJourneeCourrante{get;set;}
    public double DureeMoyenneAppel{get;set;}
    public int NbrAgentEnLigne{get;set;}

  
    /*public int CalculerNbrAppelJourneeCourrante(AppelModel[] p_appel )
    {
        return p_appel.Count(a => a.PDebutAppel.Date == DateTime.Today);
    }

    public double CalculerDureeMoyenneAppels(AppelModel[] p_appel)
    {
        var appelAvecDuree = p_appel.Where(a => a.DureeAppel.HasValue);
        return appelAvecDuree.Any() ? appelAvecDuree.Average(d => d.DureeAppel.Value.TotalSeconds) : 0;
    }

    public int CalculerNbrAgentEnLigne(AppelModel[] p_appel)
    {
        return p_appel.Count(a => !a.PFinAppel.HasValue);
    }*/
}