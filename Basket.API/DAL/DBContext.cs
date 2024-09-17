using Microsoft.EntityFrameworkCore;

namespace Basket.API.DAL;

public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
{
    public DbSet<Models.Basket> Baskets { get; set; }

    public DbSet<Models.Item> Items { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Baskets
        modelBuilder.Entity<Models.Basket>(builder =>
        {
            builder.HasIndex(p => p.UserId).IsUnique();
        });

        // Items
        modelBuilder.Entity<Models.Item>(builder =>
        {
            builder.HasIndex(p => p.ProductId);
        });

        base.OnModelCreating(modelBuilder);
    }
}