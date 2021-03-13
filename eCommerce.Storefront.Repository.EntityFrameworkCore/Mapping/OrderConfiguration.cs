using eCommerce.Storefront.Model.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(o => o.Id)
                   .HasColumnName("OrderId");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Created)
                   .HasColumnName("OrderDate")
                   .IsRequired();
            builder.Property(o => o.ShippingCharge)
                   .HasColumnName("ShippingCharge")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.Property(o => o.Status)
                   .HasColumnName("State")
                   .IsRequired();
            builder.HasOne(o => o.ShippingService);
            builder.OwnsOne(o => o.Payment, p => 
            {
                   p.Property(p => p.DatePaid).HasColumnName("PaymentDate");
                   p.Property(p => p.TransactionId).HasColumnName("PaymentTransactionId");        
                   p.Property(p => p.Merchant).HasColumnName("PaymentMerchant");
                   p.Property(p => p.Amount).HasColumnName("PaymentAmount").HasColumnType("decimal(18,2)");
            });                                         
            builder.HasOne(o => o.DeliveryAddress);                   
            builder.HasOne(o => o.Customer)
                   .WithMany(c => c.Orders)
                   .IsRequired();
            builder.HasMany(o => o.Items)
                   .WithOne(i => i.Order)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();
        }
    }
}