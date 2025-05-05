using System.Net.Http.Headers;
using System.Text.Json;
using M01_Srv_Municipalite;

namespace M01_DAL_Import_Munic_REST_JSON;

public class Importation_Muni_REST_JSON:IDepotImportationMunicipalites
{

    public static string uriRechercheParCodeGeographique = "https://www.donneesquebec.ca/{refCodeGeographique}/";
    public HttpClient httpClient;
    
    
    public Importation_Muni_REST_JSON()
    {
        httpClient = new HttpClient();
    }
    
    
    public IEnumerable<Municipalite> LireMunicipalites()
    {
        
        List<Municipalite>? municipalites = null;
        
        this.httpClient.BaseAddress = new Uri("https://www.donneesquebec.ca/");
        this.httpClient.DefaultRequestHeaders.Accept.Clear();
        this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> responseTask = httpClient.GetAsync(uriRechercheParCodeGeographique.Replace("{refCodeGeographique}",p_codeGeographique));
        responseTask.Wait();
        HttpResponseMessage response = responseTask.Result;

        if (response.IsSuccessStatusCode)
        {
            Task<string> responseTaskResult = response.Content.ReadAsStringAsync();
            responseTaskResult.Wait();
            String jsonContent = responseTaskResult.Result;
            municipalites = JsonSerializer.Deserialize<List<Municipalite>>(jsonContent);
        }
        
        return municipalites;
    }
}