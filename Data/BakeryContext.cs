using Bakery.Models;
using Microsoft.EntityFrameworkCore;
using  Bakery.Data.Configurations;
namespace Bakery.Data

{
    public class BakeryContext : DbContext
    {
        //context for product model
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data source= wwwroot/data/Bakery.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration()).Seed();
        }


    }
}