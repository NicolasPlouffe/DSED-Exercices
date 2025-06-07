namespace M06_CasUtilisation_Clients
{
    public class ManipulationsClient:IDepotClients
    {
        IDepotClients m_depotClient;

        ManipulationsClient(IDepotClients p_depotClients)
        {
            this.m_depotClient = p_depotClients;
        }

        ClientEntite IDepotClients.CreerClient(ClientEntite p_client)
        {
            return m_depotClient.CreerClient(p_client);
        }
    }
}
