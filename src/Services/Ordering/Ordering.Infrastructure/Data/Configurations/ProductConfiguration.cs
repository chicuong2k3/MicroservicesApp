

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Entities;
using Ordering.Domain.ValueObjects;
using System.Reflection.Emit;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasConversion(productId => productId.Value, id => ProductId.Generate(id));

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Price)
                .HasColumnType("decimal(19,2)")
                .IsRequired();

            builder.HasIndex(e => e.Name);

        }
    }
}
