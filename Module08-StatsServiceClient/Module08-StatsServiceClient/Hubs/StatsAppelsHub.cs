using Microsoft.AspNetCore.SignalR;

namespace Module08_StatsServiceClient.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class StatsAppelsHub : Hub
{

    public async Task CalculerNbrAppelJourneeCourrante()
    {
        int nbrAppelsJourneeCourrante = 0;
        
        await Clients.All.SendAsync("CalculerNbrAppelJourneeCourrante", nbrAppelsJourneeCourrante);
    }
    
    public async Task CalculerDureeMoyenneAppels()
    {
        float dureeMoyenneDesAppels = 0;
        
        await Clients.All.SendAsync("CalculerDureeMoyenneAppels", dureeMoyenneDesAppels);
    }

    public async override Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("Connected", Context.ConnectionId);
        
        await base.OnConnectedAsync();
    }
    
}