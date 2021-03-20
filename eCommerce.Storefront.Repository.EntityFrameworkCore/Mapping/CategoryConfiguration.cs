using System.Collections.Generic;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.Property(c => c.Id)
                   .HasColumnName("CategoryId");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasData(new List<Category>()
            {
                new Category { Id = 1, Name = "Trousers" },
                new Category { Id = 2, Name = "Shirts" },
                new Category { Id = 3, Name = "Socks" },
                new Category { Id = 4, Name = "Jackets" },
                new Category { Id = 5, Name = "T-Shirts" }
            });
        }
    }
}