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
                new { Id = (long)1, TitleId = (long)1, SizeId = (long)1 },
                new { Id = (long)2, TitleId = (long)1, SizeId = (long)2 },
                new { Id = (long)3, TitleId = (long)1, SizeId = (long)3 },
                new { Id = (long)4, TitleId = (long)1, SizeId = (long)4 },
                new { Id = (long)5, TitleId = (long)2, SizeId = (long)1 },
                new { Id = (long)6, TitleId = (long)2, SizeId = (long)2 },
                new { Id = (long)7, TitleId = (long)2, SizeId = (long)3 },
                new { Id = (long)8, TitleId = (long)2, SizeId = (long)4 },
                new { Id = (long)9, TitleId = (long)3, SizeId = (long)1 },
                new { Id = (long)10, TitleId = (long)3, SizeId = (long)2 },
                new { Id = (long)11, TitleId = (long)3, SizeId = (long)3 },
                new { Id = (long)12, TitleId = (long)3, SizeId = (long)4 },
                new { Id = (long)13, TitleId = (long)4, SizeId = (long)1 },
                new { Id = (long)14, TitleId = (long)4, SizeId = (long)2 },
                new { Id = (long)15, TitleId = (long)4, SizeId = (long)3 },
                new { Id = (long)16, TitleId = (long)4, SizeId = (long)4 },
                new { Id = (long)17, TitleId = (long)5, SizeId = (long)1 },
                new { Id = (long)18, TitleId = (long)5, SizeId = (long)2 },
                new { Id = (long)19, TitleId = (long)5, SizeId = (long)3 },
                new { Id = (long)20, TitleId = (long)5, SizeId = (long)4 },
                new { Id = (long)21, TitleId = (long)6, SizeId = (long)1 },
                new { Id = (long)22, TitleId = (long)6, SizeId = (long)2 },
                new { Id = (long)23, TitleId = (long)6, SizeId = (long)3 },
                new { Id = (long)24, TitleId = (long)6, SizeId = (long)4 }
            });
        }
    }
}