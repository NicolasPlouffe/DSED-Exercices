using Microsoft.Extensions.DependencyInjection;
using M01_Entite.IDepot;
using M01_DAL_Municipalite_SQLServer;
using M01_DAL_Import_Munic_CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSED_M01_Fichiers_Texte;
    internal static class ConfigDI
{
    public static IServiceCollection AddConfigDI(this IServiceCollection services)
    {
        // Énumération des services de l'application
        services.AddScoped<IDepotImportationMunicipalites>(provider => 
            new DepotImportationMunicipaliteCSV("/data/csv/municipalites.csv"));
        
        // Ajout correspondances vers les IDepot
        services.AddScoped<IDepotImportationMunicipalites,DepotImportationMunicipaliteCSV>();
        services.AddScoped<IDepotMunicipalites, DepotMunicipalitesSQLServer>();
        
        services.AddScoped<ITransactionBD>(provider => provider.GetService<MunicipaliteContextSQLServer>()!);
        
        return services;
    }
}