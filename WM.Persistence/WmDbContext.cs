using WM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<StateEntity> States { get; set; }
    public DbSet<ResourceEntity> Resources { get; set; }
    public DbSet<BalanceEntity> Balances { get; set; }
    public DbSet<AdmissionDocEntity> AdmissionDocs { get; set; }
    public DbSet<AdmissionResEntity> AdmissionsOfResources { get; set; }
    public DbSet<ShippingDocEntity> ShippingDocs { get; set; }
    public DbSet<ShippingResEntity> ShippingResources { get; set; }
}
