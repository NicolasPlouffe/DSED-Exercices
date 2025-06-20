using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Entite;
using Entite.IDepot;

namespace DAL;

public class ApplicationDBContexte:DbContext,ITransactionDB
{
    private IDbContextTransaction transaction;
    
    public DbSet<DTO_Compte> Comptes { get; set; }
    public DbSet<DTO_Transaction> Transactions { get; set; }
    
    public ApplicationDBContexte(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        Console.Out.WriteLine("ApplicationDBContext.ctor(...)");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configuration Compte
        modelBuilder.Entity<DTO_Compte>(entity =>
        {
            entity.ToTable("Compte", "dbo"); // Schéma explicite
            entity.HasKey(c => c.NumeroCompte); // Clé primaire GUID
        });

        // Configuration Transaction
        modelBuilder.Entity<DTO_Transaction>(entity =>
        {
            entity.ToTable("Transaction", "dbo");
            entity.HasKey(t => t.TransactionId);
        });

        base.OnModelCreating(modelBuilder);
    }



    public DbSet<DTO_Compte> Clients => Set<DTO_Compte>();
    public DbSet<DTO_Transaction> Factures => Set<DTO_Transaction>();
   
    public void BeginTransaction()
    {
        if (this.transaction is not null)
        {
            throw new InvalidOperationException("Une transaction est déjà débutée");
        }
        this.transaction = this.Database.BeginTransaction();
    }
    public void Commit()
    {
        if (this.transaction is null)
        {
            throw new InvalidOperationException("Une transaction doit être débutée");
        }
        this.transaction.Commit();
        this.transaction?.Dispose();
        this.transaction = null;    }

    public void Rollback()
    {
        if (this.transaction is null)
        {
            throw new InvalidOperationException("Une transaction doit être débutée");
        }
        this.transaction.Rollback();
        this.transaction?.Dispose();
        this.transaction = null;
    }
    
    public override void Dispose()
    {
        Console.Out.WriteLine("ApplicationDBContext.Dispose");
        this.transaction?.Dispose();
        this.transaction = null;
        base.Dispose();
    }
    
}