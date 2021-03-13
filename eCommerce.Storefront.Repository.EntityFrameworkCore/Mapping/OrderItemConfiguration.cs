using eCommerce.Storefront.Model.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eCommerce.Storefront.Repository.EntityFrameworkCore.Mapping
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(o => o.Id)
                   .HasColumnName("OrderItemId");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Qty)
                   .HasColumnName("Qty")
                   .IsRequired();
            builder.Property(o => o.Price)
                   .HasColumnName("Price")
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.Product);
            builder.HasOne(o => o.Order)
                   .WithMany(o => o.Items)
                   .IsRequired();
        }
    }
}