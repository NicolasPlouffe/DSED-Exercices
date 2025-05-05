using System.Net.Http.Headers;
using System.Text.Json;
using M01_Srv_Municipalite;

namespace M01_DAL_Import_Munic_REST_JSON;

public class DepotImportation_Muni_REST_JSON:IDepotImportationMunicipalites
{

    private readonly string uriRechercheParCodeGeographique = "https://www.donneesquebec.ca/recherche/api/action/datastore_search?resource_id=19385b4e-5503-4330-9e59-f998f5918363&limit=3000";
    private readonly HttpClient _httpClient;
    
    
    public DepotImportation_Muni_REST_JSON()
    {
        _httpClient = new HttpClient();
    }
    
    
    public IEnumerable<Municipalite> LireMunicipalites()
    {
        
        List<Municipalite>? municipalites = null;
        
        this._httpClient.BaseAddress = new Uri("https://www.donneesquebec.ca/");
        this._httpClient.DefaultRequestHeaders.Accept.Clear();
        this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Task<HttpResponseMessage> responseTask = _httpClient.GetAsync(uriRechercheParCodeGeographique);
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
    
    public IEnumerable<Municipalite> Lire(string p_jsonContent)
    {
        string json = File.ReadAllText(p_jsonContent);
        Rootobject? root = JsonSerializer.Deserialize<Rootobject>(json);

        return root!.result!.records!.Select(m =>
            new Municipalite(
                int.Parse(m!.mcode!),
                m!.munnom!,
                m!.mcourriel,
                m!.mweb,
                m!.datelec is not null ? DateOnly.FromDateTime(m.datelec.Value) : null

            )
        ).ToList();
    }
    internal class Rootobject
    {
        public Result? result { get; set; }
    }

    internal class Result
    {
        public Record[]? records { get; set; }
    }

    internal class Record
    {
        public string? mcode { get; set; }
        public string? munnom { get; set; }
        public string? mcourriel { get; set; }
        public string? mweb { get; set; }
        public DateTime? datelec { get; set; }
    }
    
    
}