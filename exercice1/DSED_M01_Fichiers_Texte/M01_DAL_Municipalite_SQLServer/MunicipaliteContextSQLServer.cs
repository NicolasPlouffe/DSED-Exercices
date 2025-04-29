using M01_Entite;
using M01_Entite.IDepot;
using Microsoft.EntityFrameworkCore;

namespace M01_DAL_Municipalite_SQLServer;

public class MunicipaliteContextSQLServer:DbContext,ITransactionBD
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


    public void Commit()
    {
        throw new NotImplementedException();
    }

    public void Rollback()
    {
        throw new NotImplementedException();
    }

    public void BeginTransaction()
    {
        throw new NotImplementedException();
    }
}