using System.Diagnostics;
using M01_Configuration_Application;
using M01_DAL_Import_Munic_CSV;
using M01_DAL_Import_Munic_JSON;
using M01_DAL_Municipalite_SQLServer;
using M01_Entite;
using M01_Srv_Municipalite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("BDMunicipalites") ?? throw new InvalidOperationException("Connection string 'BDMunicipalites' not found.");

builder.Services.AddDbContext<MunicipaliteContextSQLServer>(options =>
{
    options.UseSqlServer(connectionString)
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
#if DEBUG
        .LogTo(message => Debug.WriteLine(message), LogLevel.Information)
        .EnableSensitiveDataLogging()
#endif
        ;
});


builder.Services.AddOpenApiDocument(config => 
{
    config.Title = "Municipalités API";
    config.Version = "v1";
});

builder.Services.AddScoped<IDepotMunicipalites, DepotMunicipalitesSQLServer>();
builder.Services.Configure<DepotImportationMunicipaliteOptions>(builder.Configuration.GetSection("ImportationMunicipalites"));
builder.Services.AddScoped<TraitementImporterDonneesMunicipalite>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

DepotImportationMunicipaliteOptions depotImportationMunicipaliteOptions = builder.Configuration.GetSection("ImportationMunicipalites").Get<DepotImportationMunicipaliteOptions>() ?? throw new InvalidOperationException("ImportationMunicipalites section not found.");
string fullPath = Path.Combine(Directory.GetCurrentDirectory(), depotImportationMunicipaliteOptions.FilePath);
if (!File.Exists(fullPath))
{
    throw new InvalidOperationException($"Fichier CSV manquant : {fullPath}");
}

switch (Path.GetExtension(depotImportationMunicipaliteOptions.FilePath))
{
    case ".csv":
        builder.Services.AddScoped<IDepotImportationMunicipalites, DepotImportationMunicipaliteCSV>();
        break;
    case ".json":
        builder.Services.AddScoped<IDepotImportationMunicipalites, DepotImportationMunicipaliteJSON>();
        break;
    default:
        throw new InvalidOperationException(
            $"Le fichier {depotImportationMunicipaliteOptions.FilePath} n'est pas un fichier CSV ou JSON.");
}
var app = builder.Build();

using (IServiceScope serviceScope = app.Services.CreateScope())
{
    IServiceProvider services = serviceScope.ServiceProvider;
    TraitementImporterDonneesMunicipalite tidm = services.GetRequiredService<TraitementImporterDonneesMunicipalite>();
    StatistiquesImportationDonnees sid = tidm.Executer();}


// Gestion des erreures
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseOpenApi();
app.UseSwaggerUi(config => // ← Méthode renommée dans NSwag 14
{
    config.Path = "/swagger";
    // Optionnel mais recommandé :
    config.DocumentPath = "/swagger/v1/swagger.json"; 
});

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();