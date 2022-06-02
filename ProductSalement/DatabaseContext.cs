using Microsoft.EntityFrameworkCore;
using ProductSalement.Models;

namespace ProductSalement
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesPoint> SalesPoints { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
