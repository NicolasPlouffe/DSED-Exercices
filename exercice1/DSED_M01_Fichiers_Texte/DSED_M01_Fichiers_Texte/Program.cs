using DSED_M01_Fichiers_Texte;
using M01_DAL_Municipalite_SQLServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using M01_Entite;
using M01_Entite.IDepot;
using M01_DAL_Import_Munic_CSV;
using M01_DAL_Import_Munic_JSON;
using M01_Entite.IDepot;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using M01_Srv_Municipalite;

string cheminCSV = "../M01_FichiersImportes/MUN.csv";

HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(args);

IHostEnvironment env = hostApplicationBuilder.Environment;

hostApplicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

string connectionString = hostApplicationBuilder.Configuration.GetConnectionString("PersonnesConnection") ?? throw new InvalidOperationException("Connection string 'PersonnesConnection' not found.");

// ajoute les services énuméré dans la classe - ajoute tous les scopes
hostApplicationBuilder.Services.AddConfigDI(connectionString,cheminCSV);

IHost host = hostApplicationBuilder.Build();

using (IServiceScope scope = host.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var context = serviceProvider.GetRequiredService<MunicipaliteContextSQLServer>();
    var depotImport = serviceProvider.GetRequiredService<IDepotImportationMunicipalites>();
    var depotMunicipalite = serviceProvider.GetRequiredService<IDepotMunicipalites>();

    TraitementImporterDonneesMunicipalite traitementBL = new TraitementImporterDonneesMunicipalite(depotImport,depotMunicipalite);
    
    Console.WriteLine(traitementBL.Executer().ToString());
    
}


