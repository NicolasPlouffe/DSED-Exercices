using DSED_M05_Model;
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace DSED_M05_Client
{
    class Program
    {

        static void Main(string[] args)
        {
            Thread.Sleep(5000);
            
            Binding binding = new BasicHttpBinding();
            EndpointAddress endpoint = new EndpointAddress(new Uri("http://localhost:5135/OperationService.asmx"));
            ChannelFactory<IOperationsService> channelFactory = new ChannelFactory<IOperationsService>(binding, endpoint);
            IOperationsService operationsService = channelFactory.CreateChannel();

            string echo = operationsService.Echo("Bonjour DSED !");
            Console.Out.WriteLine($"Echo : {echo}");
            try
            {
                float addition = operationsService.Additionner(2, 2);
                Console.Out.WriteLine($"Somme est : {addition}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Interet1 : {ex.Message}");
            }

            int nombreEssais = 0;
            int nombreMaximumEssais = 3;
            bool appelEffectue = false;
            float somme = -1f;
            while (!appelEffectue && nombreEssais < nombreMaximumEssais)
            {
                ++nombreEssais;
                try
                {
                    somme = operationsService.Additionner(2,3);
                    appelEffectue = true;
                    Console.Out.WriteLine($"Deuxieme somme : {somme}");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Interet2 : {ex.Message} - nombreEssais : {nombreEssais}");
                    if (nombreEssais >= nombreMaximumEssais)
                    {
                        throw;
                    }
                }
            }
            Console.In.ReadLine();
        }
    }
}