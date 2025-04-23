using M01_Entite;
using Microsoft.EntityFrameworkCore;

namespace M01_DAL_Municipalite_SQLServer;

public class MunicipaliteContextSQLServer:DbContext
{
  
    public DbSet<MunicipaliteDTO> Municipalites { get; set; }
    
    public MunicipaliteContextSQLServer(DbContextOptions<MunicipaliteContextSQLServer> options)
        : base(options)
    {
        ;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
           // optionsBuilder.UseSqlServer("Server=localhost;Database=MunicipaliteDB;User Id=sa;Password=your_password;");
        }
    }
     
    
}