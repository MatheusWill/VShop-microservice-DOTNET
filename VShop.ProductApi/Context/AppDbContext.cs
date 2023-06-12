using Microsoft.EntityFrameworkCore;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        //Category

        mb.Entity<Category>().HasKey(c => c.CategoryId);

        mb.Entity<Category>().Property(c => c.ExternalId).HasMaxLength(36);

        mb.Entity<Category>().
            Property(c => c.Name).
                HasMaxLength(100).
                    IsRequired();

        mb.Entity<Category>().
            HasMany(x => x.Products).
            WithOne(c => c.Category).
            IsRequired().
            OnDelete(DeleteBehavior.Cascade);

        mb.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Material Escolar",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Acessórios",
                }
            );

        //Product

        mb.Entity<Product>().
            Property(p => p.Name).
                HasMaxLength(100).
                    IsRequired();

        mb.Entity<Product>().
           Property(p => p.Description).
               HasMaxLength(255).
                   IsRequired();

        mb.Entity<Product>().
           Property(p => p.ImageURL).
               HasMaxLength(255).
                   IsRequired();

        mb.Entity<Product>().
           Property(p => p.Price).
               HasPrecision(12, 2);

        mb.Entity<Product>().Property(p => p.ExternalId).HasMaxLength(36);

        mb.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Caderno 20 matérias",
                    Price = 20,
                    Description = "Caderno grande 20 materias",
                    Stock = 10,
                    ImageURL = "caderno.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 2,
                    Name = "Estojo",
                    Price = 5,
                    Description = "Estojo para lapis",
                    Stock = 23,
                    ImageURL = "Estojo.jpg",
                    CategoryId = 1
                },
                new Product
                {
                    Id = 3,
                    Name = "Prisilha",
                    Price = 1.50M,
                    Description = "Prisilha para cabelo",
                    Stock = 36,
                    ImageURL = "Prisilha.jpg",
                    CategoryId = 2
                }
            );
    }
}
