using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatsAppelClient.Depot;
using Microsoft.AspNetCore.Mvc;

namespace StatsAppelClient.Hubs

{
    public class StatsAppelHub: Hub
    {
        private AppelDepot _appelDepot;
       
        public StatsAppelHub(AppelDepot p_appel)
        {
            this._appelDepot = p_appel;
        }

        public async override Task OnConnectedAsync()
        {

            await Clients.Caller.SendAsync("Connected",
                this._appelDepot.CalculerDureeMoyenneAppels(),
                this._appelDepot.CalculerNbrAgentEnLigne(),
                this._appelDepot.CalculerNbrAppelJourneeCourrante());  

            await base.OnConnectedAsync();
        }

    }
}
