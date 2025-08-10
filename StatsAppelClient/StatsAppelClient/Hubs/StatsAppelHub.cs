using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsAppelClient.Depot;
using Microsoft.AspNetCore.Mvc;
using StatsAppelClient.Services_BL;
namespace StatsAppelClient.Hubs

{
    public class StatsAppelHub: Hub
    {
        private readonly StatsAppelService _statsAppelService;
       
        public StatsAppelHub(StatsAppelService p_statsAppelService)
        {
            this._statsAppelService = p_statsAppelService;
        }

        public async override Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("MajStats",
                _statsAppelService.CalculerDureeMoyenneAppels(),
                _statsAppelService.CalculerNbrAgentEnLigne(),
                _statsAppelService.CalculerNbrAppelJourneeCourrante()
                );

            await base.OnConnectedAsync();
        }
    }
}
