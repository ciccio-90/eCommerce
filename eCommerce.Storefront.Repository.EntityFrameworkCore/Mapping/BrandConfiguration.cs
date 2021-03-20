using System.Collections.Generic;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.Property(b => b.Id)
                   .HasColumnName("BrandId");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(b => b.Name).IsUnique();
            builder.HasData(new List<Brand>()
            {
                new Brand { Id = 1, Name = "Levi" },
                new Brand { Id = 2, Name = "Bench" }
            });
        }
    }
}