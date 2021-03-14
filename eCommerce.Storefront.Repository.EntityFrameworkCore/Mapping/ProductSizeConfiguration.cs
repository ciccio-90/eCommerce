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
        }
    }
}