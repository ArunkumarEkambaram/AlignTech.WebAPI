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

        //Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Product          
            var product = modelBuilder.Entity<Product>();
            product.Property(x => x.Name).IsRequired().HasMaxLength(100);
            product.Property(x => x.Description).HasMaxLength(500).HasColumnType("varchar");
            product.Property(x => x.UnitPrice).HasColumnType("decimal(18,2)");
            product.Property(x => x.AddedDate).HasDefaultValueSql("Getdate()");
        }
    }
}
