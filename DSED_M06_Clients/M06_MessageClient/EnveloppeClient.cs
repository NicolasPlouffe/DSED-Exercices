using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M06_MessageClient
{
    public class EnveloppeClient
    {
        string Action { get; set; }
        string ActionId { get; set; }
        MessageClient Client { get; set; }


        public EnveloppeClient( string p_action, string p_id, MessageClient p_message)
        {
            this.Action = p_action;
            this.ActionId = p_id;
            this.Client = p_message;
        }
    }
}
