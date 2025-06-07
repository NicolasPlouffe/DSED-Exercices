using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using M06_CasUtilisation_Clients;

namespace M06_DAL_Client_SQLServeur
{
    public class ClientSQLServeurDTO
    {
        Guid ClientSQLDTOId { get; set; }
        string Prenom { get; set; }
        string Nom { get; set; }
        string Courriel { get; set; }
        string NumeroTelephone { get; set; }

        ClientSQLServeurDTO(ClientEntite p_clientEntite)
        {
            this.ClientSQLDTOId = p_clientEntite.Identifiant;
            this.Prenom = p_clientEntite.Prenom;
            this.Nom = p_clientEntite.Nom;
            this.Courriel = p_clientEntite.Courriel;
            this.NumeroTelephone = p_clientEntite.NumeroTelephone;
        }

    }
}
