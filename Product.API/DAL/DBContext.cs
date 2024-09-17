using Microsoft.EntityFrameworkCore;

namespace Product.API.DAL;

public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
{
    public DbSet<Models.Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Products
        modelBuilder.Entity<Models.Product>(builder =>
        {
            builder.HasIndex(p => p.Code).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}
