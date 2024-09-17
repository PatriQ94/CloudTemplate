using Microsoft.EntityFrameworkCore;

namespace Product.API.DAL;

public class DBContext(DbContextOptions<DBContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        base.OnConfiguring(options);
    }
}
