using DSED_M01_Fichiers_Texte;
using M01_DAL_Municipalite_SQLServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using M01_Entite.IDepot;

using M01_Srv_Municipalite;

//string cheminCSV = "/home/nico/Documents/Dev/AEC/DSED/420-W47-SF-main/DSED-Exercices/DSED-Exercices/exercice1/DSED_M01_Fichiers_Texte/M01_FichiersImportes/MUN.csv";

string dossierExecution = AppDomain.CurrentDomain.BaseDirectory;
string racineProjet = Path.GetFullPath(Path.Combine(dossierExecution, "..", "..", "..", "..")).Replace('\\', '/');
string cheminCSV = Path.Combine(racineProjet, "M01_FichiersImportes", "MUN.csv");

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


