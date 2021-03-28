using eCommerce.Storefront.Model.Shipping;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class DeliveryOptionConfiguration : IEntityTypeConfiguration<DeliveryOption>
    {
        public void Configure(EntityTypeBuilder<DeliveryOption> builder)
        {
            builder.ToTable("DeliveryOptions");
            builder.Property(d => d.Id)
                   .HasColumnName("DeliveryOptionId");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.FreeDeliveryThreshold)
                   .HasColumnName("FreeDeliveryThreshold")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(d => d.Cost)
                   .HasColumnName("Cost")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.HasOne(d => d.ShippingService);
            builder.HasData(new { Id = (long)1, FreeDeliveryThreshold = 29.99M, Cost = 3.99M, ShippingServiceId = (long)1 });
        }
    }
}