using System.Collections.Generic;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.ToTable("Sizes");
            builder.Property(s => s.Id)
                   .HasColumnName("SizeId");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(s => s.Name).IsUnique();
            builder.HasData(new List<ProductSize>()
            {
                new ProductSize { Id = 1, Name = "L" },
                new ProductSize { Id = 2, Name = "XL" },
                new ProductSize { Id = 3, Name = "M" },
                new ProductSize { Id = 4, Name = "S" }
            });
        }
    }
}