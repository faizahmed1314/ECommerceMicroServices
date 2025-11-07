using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enum;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastucture.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .IsRequired();

            builder.HasMany(o => o.OrderItems)
                   .WithOne()
                   .HasForeignKey(oi => oi.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Order.OrderName))
                    .HasMaxLength(100)
                    .IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                    addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                    addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                    addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                    addressBuilder.Property(a => a.Country).HasMaxLength(50);
                    addressBuilder.Property(a => a.State).HasMaxLength(50);
                    addressBuilder.Property(a => a.PostalCode).HasMaxLength(50);
                });

            builder.ComplexProperty(
                o => o.BillingAddress, addressBuilder =>
                {
                    addressBuilder.Property(a => a.FirstName).HasMaxLength(50).IsRequired();
                    addressBuilder.Property(a => a.LastName).HasMaxLength(50).IsRequired();
                    addressBuilder.Property(a => a.EmailAddress).HasMaxLength(50);
                    addressBuilder.Property(a => a.AddressLine).HasMaxLength(180).IsRequired();
                    addressBuilder.Property(a => a.Country).HasMaxLength(50);
                    addressBuilder.Property(a => a.State).HasMaxLength(50);
                    addressBuilder.Property(a => a.PostalCode).HasMaxLength(50);
                });

            builder.ComplexProperty(
                o => o.Payment, paymentBuilder =>
                {
                    paymentBuilder.Property(p => p.CardNumber).HasMaxLength(25).IsRequired();
                    paymentBuilder.Property(p => p.CardHolderName).HasMaxLength(100);
                    paymentBuilder.Property(p => p.Expiration).HasMaxLength(5).IsRequired();
                    paymentBuilder.Property(p => p.CVV).HasMaxLength(3).IsRequired();
                    paymentBuilder.Property(p => p.PaymentMethod).IsRequired();
                });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.OrderDate).IsRequired();
            builder.Property(o => o.TotalAmount);


        }
    }
}
