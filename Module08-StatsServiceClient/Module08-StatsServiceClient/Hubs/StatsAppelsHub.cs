using Microsoft.AspNetCore.SignalR;
using Module08_StatsServiceClient.Services;

namespace Module08_StatsServiceClient.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StatsAppelsHub : Hub
{

    private readonly AppelRepository _appelRepository;

    public StatsAppelsHub(AppelRepository appelRepository)
    {
        this._appelRepository = appelRepository;
    }
    public async Task CalculerNbrAppelJourneeCourrante()
    {
        int nbrAppelsJourneeCourrante = _appelRepository.Appels.Count(a => a.PDebutAppel.Date == DateTime.Today);
        
        await Clients.All.SendAsync("CalculerNbrAppelJourneeCourrante", nbrAppelsJourneeCourrante);
    }
    
    public async Task CalculerDureeMoyenneAppels()
    {
         double dureeMoyenneDesAppels = _appelRepository.Appels.Where(a => a.DureeAppel.HasValue).Average(d => d.DureeAppel.Value.TotalSeconds);
        
        await Clients.All.SendAsync("CalculerDureeMoyenneAppels", dureeMoyenneDesAppels);
    }

    public async override Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("Connected", Context.ConnectionId);
        
        await base.OnConnectedAsync();
    }
    
}