using Microsoft.EntityFrameworkCore;

namespace M01_DAL_Municipalite_SQLServer
{
    public class MunicipaliteContextSQLServer : DbContext//: IdentityDbContext
    {
        public DbSet<ClefAPI_DTO> ClefApi { get; set; }
        public DbSet<MunicipaliteDTO> Municipalite { get; set; }
        
        public MunicipaliteContextSQLServer(DbContextOptions options)
        : base(options)
        {
            ;
        }
    }
}
