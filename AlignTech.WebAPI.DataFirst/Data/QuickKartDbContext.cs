using AlignTech.WebAPI.DataFirst.Configurations;
using AlignTech.WebAPI.DataFirst.DTOs;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AlignTech.WebAPI.DataFirst.Data;

public partial class QuickKartDbContext : DbContext
{
    public QuickKartDbContext(DbContextOptions<QuickKartDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CardDetail> CardDetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public DbSet<ProductAndCategoryDto> ProductAndCategoryDto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new CardDetailConfiguration());

        //Configuring ProductCategoryDto
        modelBuilder.Entity<ProductAndCategoryDto>(x => x
        .HasNoKey() //There is no Primary Key
        .ToView(null)//No View
        .Property("Quantity").HasColumnName("QuantityAvailable")
        );
    }

}
