using Microsoft.AspNetCore.SignalR;

namespace Module08_StatsServiceClient.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AppelsHub : Hub
{
    // Afficher les stats courrantes
    // Sur JS
   /*nbr appele en jouree courrante requete linq
    
    // Temps moyen passe par appel : 
    // list des appels.
     finAppel pas null
     moyenne duree. 
    
    // Nbr agents en cours
    // Equivalent au nbr appel en cours
    // List appels Where FinAppel == null.
    // count. 
}