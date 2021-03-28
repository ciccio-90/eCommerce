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
                new { Id = (long)1, Name = "506 Stretch Diamond", Price = 27.99M, BrandId = (long)1, CategoryId = (long)1, ColorId = (long)2 },
                new { Id = (long)2, Name = "506 Dark Stuff Straight", Price = 25.99M, BrandId = (long)1, CategoryId = (long)1, ColorId = (long)1 },
                new { Id = (long)3, Name = "512 Bootcut Jeans", Price = 22.99M, BrandId = (long)1, CategoryId = (long)1, ColorId = (long)2 },
                new { Id = (long)4, Name = "Lucian Straight Jeans", Price = 22.99M, BrandId = (long)2, CategoryId = (long)1, ColorId = (long)2 },
                new { Id = (long)5, Name = "Lucian Straight Jeans", Price = 22.99M, BrandId = (long)2, CategoryId = (long)1, ColorId = (long)3 },
                new { Id = (long)6, Name = "Lucian Straight Jeans", Price = 22.99M, BrandId = (long)2, CategoryId = (long)1, ColorId = (long)4 }
            });
        }
    }
}