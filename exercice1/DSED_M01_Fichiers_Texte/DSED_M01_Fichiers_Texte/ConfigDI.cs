using Microsoft.Extensions.DependencyInjection;
using M01_Entite.IDepot;
using M01_DAL_Municipalite_SQLServer;
using M01_DAL_Import_Munic_CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DSED_M01_Fichiers_Texte;
    internal static class ConfigDI
    {
        
        // mettre dans import csv
        private static readonly Regex cheminCSV = new Regex(@"^.*[\/\\][^\/\\]+\.csv$");
    public static IServiceCollection AddConfigDI(this IServiceCollection services, string connectionString, string p_cheminCSV)
    {
       
        if (!cheminCSV.IsMatch(p_cheminCSV)){throw new FormatException("Le chemin cette CSV n'est pas valide");}
        
        // Énumération des services de l'application
        
        // DB Contect
        services.AddDbContext<MunicipaliteContextSQLServer>(option => option.UseSqlServer(connectionString));
        
        // Importation et lecture de CSV
        services.AddScoped<IDepotImportationMunicipalites>(provider => 
            new DepotImportationMunicipaliteCSV(p_cheminCSV));
        
        // Ajout correspondances vers les IDepot
        services.AddScoped<IDepotMunicipalites, DepotMunicipalitesSQLServer>();
        
        //
        services.AddScoped<ITransactionBD>(provider => provider.GetService<MunicipaliteContextSQLServer>()!);
        
        return services;
    }
}