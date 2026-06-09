using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Core.Entities.StoreType> StoreTypes => Set<Core.Entities.StoreType>();
    public DbSet<Core.Entities.User> Users => Set<Core.Entities.User>();
    public DbSet<Core.Entities.Store> Stores => Set<Core.Entities.Store>();
    public DbSet<Core.Entities.Category> Categories => Set<Core.Entities.Category>();
    public DbSet<Core.Entities.Product> Products => Set<Core.Entities.Product>();
    public DbSet<Core.Entities.Order> Orders => Set<Core.Entities.Order>();
    public DbSet<Core.Entities.OrderItem> OrderItems => Set<Core.Entities.OrderItem>();
    public DbSet<Core.Entities.Courier> Couriers => Set<Core.Entities.Courier>();
    public DbSet<Core.Entities.Review> Reviews => Set<Core.Entities.Review>();
    public DbSet<Core.Entities.FileModel> Files => Set<Core.Entities.FileModel>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
