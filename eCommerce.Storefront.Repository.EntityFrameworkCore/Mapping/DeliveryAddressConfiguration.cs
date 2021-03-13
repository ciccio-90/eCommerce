using eCommerce.Storefront.Model.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class DeliveryAddressConfiguration : IEntityTypeConfiguration<DeliveryAddress>
    {
        public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
        {
            builder.ToTable("CustomerDeliveryAddresses");
            builder.Property(d => d.Id)
                   .HasColumnName("DeliveryId");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(d => d.AddressLine)
                   .HasColumnName("AddressLine")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(d => d.City)
                   .HasColumnName("City")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(d => d.State)
                   .HasColumnName("State")
                   .HasMaxLength(50);
            builder.Property(d => d.ZipCode)
                   .HasColumnName("ZipCode")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.Property(d => d.Country)
                   .HasColumnName("Country")
                   .HasMaxLength(50)
                   .IsRequired();
            builder.HasOne(d => d.Customer);            
        }
    }
}