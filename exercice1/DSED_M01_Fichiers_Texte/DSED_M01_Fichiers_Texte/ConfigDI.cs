using Microsoft.Extensions.DependencyInjection;
using M01_Entite.IDepot;
using M01_DAL_Municipalite_SQLServer;
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
        
        // Ajout correspondances vers les IDepot
        services.AddScoped<IDepotImportationMunicipalites>();
        services.AddScoped<IDepotMunicipalites>();
        
        
        services.AddScoped<ITransactionBD>(provider => provider.GetService<MunicipaliteContextSQLServer>()!);
        
            
            
            
            return services;
    }
}