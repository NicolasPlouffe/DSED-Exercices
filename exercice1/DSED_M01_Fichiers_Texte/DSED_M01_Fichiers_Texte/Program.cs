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

HostApplicationBuilder hostApplicationBuilder = Host.CreateApplicationBuilder(args);

IHostEnvironment env = hostApplicationBuilder.Environment;

hostApplicationBuilder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

string connectionString = hostApplicationBuilder.Configuration.GetConnectionString("PersonnesConnection") ?? throw new InvalidOperationException("Connection string 'PersonnesConnection' not found.");

hostApplicationBuilder.Services.AddDbContext<MunicipaliteContextSQLServer>(option => option.UseSqlServer(connectionString));

hostApplicationBuilder.Services.AddScoped<IDepotImportationMunicipalites,DepotImportationMunicipaliteJSON>();
hostApplicationBuilder.Services.AddScoped<IDepotImportationMunicipalites,DepotImportationMunicipaliteCSV>();

IHost host = hostApplicationBuilder.Build();

using (IServiceScope scope = host.Services.CreateScope())
{
    IServiceProvider serviceProvider = scope.ServiceProvider;
}

