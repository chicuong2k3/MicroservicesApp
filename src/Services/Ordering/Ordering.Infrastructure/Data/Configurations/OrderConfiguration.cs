using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasConversion(orderId => orderId.Value, id => OrderId.Generate(id));


            builder.ComplexProperty(e => e.OrderName, buildAction =>
            {
                buildAction.Property(x => x.Value)
                            .HasColumnName(nameof(OrderName))
                            .HasMaxLength(100)
                            .IsRequired();
            });

            builder.ComplexProperty(e => e.ShippingAddress, buildAction =>
            {
                buildAction.Property(x => x.City)
                            .HasMaxLength(50)
                            .IsRequired();
                buildAction.Property(x => x.District)
                            .HasMaxLength(50)
                            .IsRequired();
                buildAction.Property(x => x.Town)
                            .HasMaxLength(50)
                            .IsRequired();
                buildAction.Property(x => x.AddressLine)
                            .HasMaxLength(250);
            });

            builder.ComplexProperty(e => e.Payment, buildAction =>
            {
                buildAction.Property(x => x.Title)
                            .HasMaxLength(50)
                            .IsRequired();

                buildAction.Property(x => x.PaymentMethod)
                            .HasMaxLength(50)
                            .IsRequired();

                buildAction.Property(x => x.DatePaid);  
            });

            builder.Property(e => e.Status)
                .HasDefaultValue(OrderStatus.Pending)
                .HasConversion(s => s.ToString(), dbS => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbS));

            builder.Property(e => e.TotalPrice)
                .IsRequired(); ;

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(e => e.CustomerId).IsRequired();

            builder.HasMany(e => e.OrderItems)
                .WithOne()
                .HasForeignKey(e => e.OrderId);

        }
    }
}
