using StatsAppelClient.Depot;
using StatsAppelClient.Models;
namespace StatsAppelClient.Services_BL;

public class StatsAppelService
{
    private readonly AppelDepot _appelDepot;

    public StatsAppelService(AppelDepot p_appelDepot)
    {
        _appelDepot = p_appelDepot;
    }
    
    public int CalculerNbrAppelJourneeCourrante()
    {
        return _appelDepot.Appels.Count(a => a.PDebutAppel.Date == DateTime.Today);
    }

    public double CalculerDureeMoyenneAppels()
    {
        var appelAvecDuree = _appelDepot.Appels.Where(a => a.DureeAppel.HasValue);
        return appelAvecDuree.Any() ? appelAvecDuree.Average(d => d.DureeAppel.Value.TotalSeconds) : 0;
    }

    public int CalculerNbrAgentEnLigne()
    {
        return _appelDepot.Appels.Count(a => !a.PFinAppel.HasValue);
    }

    public StatsAppelModel ObtenirToutesLesStatistiques()
    {
        return new StatsAppelModel
        {
            NbrAppelJourneeCourrante = CalculerNbrAppelJourneeCourrante(),
            DureeMoyenneAppel = CalculerDureeMoyenneAppels(),
            NbrAgentEnLigne = CalculerNbrAgentEnLigne()
        };
    }
}