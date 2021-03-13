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
        }
    }
}