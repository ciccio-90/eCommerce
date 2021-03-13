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
        }
    }
}