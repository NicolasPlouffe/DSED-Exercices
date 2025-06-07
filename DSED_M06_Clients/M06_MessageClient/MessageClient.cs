namespace M06_MessageClient
{
    public class MessageClient
    {
       public  string Prenom { get; set; }
        public string Nom { get; set; }
        public string Courriel { get; set; }
        public string NumeroTelephone { get; set; }


        public MessageClient(string p_prenom, string p_nom, string p_courriel, string p_numeroTelephone)
        {
            this.Prenom = p_prenom;
            this.Nom = p_nom;
            this.Courriel = p_courriel;
            this.NumeroTelephone = p_numeroTelephone;
        }
        public MessageClient()
        {
            ;
        }

    }
}
