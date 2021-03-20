using System.Collections.Generic;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class ProductTitleConfiguration : IEntityTypeConfiguration<ProductTitle>
    {
        public void Configure(EntityTypeBuilder<ProductTitle> builder)
        {
            builder.ToTable("ProductTitles");
            builder.Property(t => t.Id)
                   .HasColumnName("ProductTitleId");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Price)
                   .HasColumnName("Price")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(t => t.Name)
                   .HasColumnName("ProductName")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasOne(t => t.Color);
            builder.HasOne(t => t.Brand);
            builder.HasOne(t => t.Category);
            builder.HasMany(t => t.Products)
                   .WithOne(p => p.Title)
                   .IsRequired();
            builder.HasData(new List<object>
            {
                new { Id = 1, Name = "506 Stretch Diamond", Price = 27.99M, BrandId = 1, CategoryId = 1, ColorId = 2 },
                new { Id = 2, Name = "506 Dark Stuff Straight", Price = 25.99M, BrandId = 1, CategoryId = 1, ColorId = 1 },
                new { Id = 3, Name = "512 Bootcut Jeans", Price = 22.99M, BrandId = 1, CategoryId = 1, ColorId = 2 },
                new { Id = 4, Name = "Lucian Straight Jeans", Price = 22.99M, BrandId = 2, CategoryId = 1, ColorId = 2 },
                new { Id = 5, Name = "Lucian Straight Jeans", Price = 22.99M, BrandId = 2, CategoryId = 1, ColorId = 3 },
                new { Id = 6, Name = "Lucian Straight Jeans", Price = 22.99M, BrandId = 2, CategoryId = 1, ColorId = 4 }
            });
        }
    }
}