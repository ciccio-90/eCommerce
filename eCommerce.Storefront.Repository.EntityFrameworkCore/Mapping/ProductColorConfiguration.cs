using System.Collections.Generic;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.ToTable("Colors");
            builder.Property(c => c.Id)
                   .HasColumnName("ColorId");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasData(new List<ProductColor>()
            {
                new ProductColor { Id = (long)1, Name = "Black" },
                new ProductColor { Id = (long)2, Name = "Blue" },
                new ProductColor { Id = (long)3, Name = "Red" },
                new ProductColor { Id = (long)4, Name = "Green" }
            });
        }
    }
}