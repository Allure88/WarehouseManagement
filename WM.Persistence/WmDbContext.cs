using Microsoft.EntityFrameworkCore;
using WM.Domain.Entities;

namespace WM.Persistence;

public class WmDbContext : DbContext
{
    public WmDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
        //Database.EnsureCreated(); 
    }

    public DbSet<ClientEntity> Clients { get; set; }
    public DbSet<UnitEntity> UnitsOfMeasurement { get; set; }
    //public DbSet<StateEntity> States { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<BalanceEntity> Balances { get; set; }
    public DbSet<AdmissionDocEntity> AdmissionDocs { get; set; }
    //public DbSet<AdmissionResEntity> AdmissionsOfResources { get; set; }
    public DbSet<ShippingDocEntity> ShippingDocs { get; set; }
    //public DbSet<ShippingResEntity> ShippingResources { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientEntity>()
            .HasIndex(e => e.Name).IsUnique();

        modelBuilder.Entity<UnitEntity>()
             .HasIndex(e => e.Name).IsUnique();

        modelBuilder.Entity<ResourceEntity>()
             .HasIndex(e => e.Name).IsUnique();

        //modelBuilder.Entity<BalanceEntity>()
        //  .HasAlternateKey(e => new { e.Resource, e.UnitOfMeasurement });

        modelBuilder.Entity<AdmissionDocEntity>()
           .HasAlternateKey(e => e.Number);

        modelBuilder.Entity<ShippingDocEntity>()
          .HasAlternateKey(e => e.Number);

        modelBuilder.Entity<AdmissionResEntity>()
            .HasOne(am => am.Resource)
            .WithMany(r => r.AdmissionMovements)
            .HasForeignKey(c => c.ResourceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<AdmissionResEntity>()
            .HasOne(am => am.UnitOfMeasurement)
            .WithMany(u => u.AdmissionMovements)
            .HasForeignKey(c => c.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();


        modelBuilder.Entity<ShippingResEntity>()
            .HasOne(sm => sm.Resource)
            .WithMany(r=>r.ShippingMovements)
            .HasForeignKey(c => c.ResourceId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<ShippingResEntity>()
            .HasOne(rm => rm.UnitOfMeasurement)
            .WithMany(u=>u.ShippingMovements)
            .HasForeignKey(c => c.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();


        modelBuilder.Entity<BalanceEntity>()
          .HasOne(b => b.Resource)
          .WithMany(r => r.Balances)
          .HasForeignKey(c => c.ResourceId)
          .OnDelete(DeleteBehavior.Restrict)
          .IsRequired();

        modelBuilder.Entity<BalanceEntity>()
            .HasOne(b => b.UnitOfMeasurement)
            .WithMany(u => u.Balances)
            .HasForeignKey(c => c.UnitOfMeasurementId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
