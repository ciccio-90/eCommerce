using System.Collections.Generic;
using eCommerce.Storefront.Model.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.Property(p => p.Id)
                   .HasColumnName("ProductId");
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Size);
            builder.HasOne(p => p.Title)
                   .WithMany(p => p.Products)
                   .IsRequired();
            builder.HasData(new List<object>
            {
                new { Id = 1, TitleId = 1, SizeId = 1 },
                new { Id = 2, TitleId = 1, SizeId = 2 },
                new { Id = 3, TitleId = 1, SizeId = 3 },
                new { Id = 4, TitleId = 1, SizeId = 4 },
                new { Id = 5, TitleId = 2, SizeId = 1 },
                new { Id = 6, TitleId = 2, SizeId = 2 },
                new { Id = 7, TitleId = 2, SizeId = 3 },
                new { Id = 8, TitleId = 2, SizeId = 4 },
                new { Id = 9, TitleId = 3, SizeId = 1 },
                new { Id = 10, TitleId = 3, SizeId = 2 },
                new { Id = 11, TitleId = 3, SizeId = 3 },
                new { Id = 12, TitleId = 3, SizeId = 4 },
                new { Id = 13, TitleId = 4, SizeId = 1 },
                new { Id = 14, TitleId = 4, SizeId = 2 },
                new { Id = 15, TitleId = 4, SizeId = 3 },
                new { Id = 16, TitleId = 4, SizeId = 4 },
                new { Id = 17, TitleId = 5, SizeId = 1 },
                new { Id = 18, TitleId = 5, SizeId = 2 },
                new { Id = 19, TitleId = 5, SizeId = 3 },
                new { Id = 20, TitleId = 5, SizeId = 4 },
                new { Id = 21, TitleId = 6, SizeId = 1 },
                new { Id = 22, TitleId = 6, SizeId = 2 },
                new { Id = 23, TitleId = 6, SizeId = 3 },
                new { Id = 24, TitleId = 6, SizeId = 4 }
            });
        }
    }
}