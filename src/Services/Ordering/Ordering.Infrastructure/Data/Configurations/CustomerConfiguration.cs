using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasConversion(customerId => customerId.Value, id => CustomerId.Generate(id));

            builder.Property(e => e.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(e => e.UserName);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);

        }
    }
}
