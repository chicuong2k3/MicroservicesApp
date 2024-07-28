using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasConversion(orderItemId => orderItemId.Value, id => OrderItemId.Generate(id));

            

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(e => e.ProductId).IsRequired();

        }
    }
}
