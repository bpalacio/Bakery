using Bakery.Data.Configurations;
using Bakery.Models;
using Microsoft.EntityFrameworkCore;
namespace Bakery.Data

{
  public class BakeryContext : DbContext {
    //context for product model
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }

        public BakeryContext(DbContextOptions<BakeryContext> options)
             : base(options)
        {
        }

    protected override void OnModelCreating (ModelBuilder modelBuilder) {
      modelBuilder.ApplyConfiguration (new ProductConfiguration ()).Seed ();
    }

  }
}