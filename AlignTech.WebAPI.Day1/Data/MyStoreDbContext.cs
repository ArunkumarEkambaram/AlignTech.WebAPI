using AlignTech.WebAPI.Day1.Configurations;
using AlignTech.WebAPI.Day1.Models;
using Microsoft.EntityFrameworkCore;

namespace AlignTech.WebAPI.Day1.Data
{
    public class MyStoreDbContext : DbContext
    {
        public MyStoreDbContext(DbContextOptions<MyStoreDbContext> option) : base(option)
        {

        }

        //Table Info
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
