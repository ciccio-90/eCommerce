using eCommerce.Storefront.Model.Shipping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class ShippingServiceConfiguration : IEntityTypeConfiguration<ShippingService>
    {
        public void Configure(EntityTypeBuilder<ShippingService> builder)
        {
            builder.ToTable("CourierServices");
            builder.Property(s => s.Id)
                   .HasColumnName("CourierServiceId");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Code)
                   .HasColumnName("ServiceCode")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(s => s.Description)
                   .HasColumnName("ServiceDescription")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasOne(s => s.Courier)
                   .WithMany(s => s.Services)
                   .IsRequired();
        }
    }
}