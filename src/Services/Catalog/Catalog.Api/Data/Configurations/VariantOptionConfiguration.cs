using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.Configurations
{
    public class VariantConfigVariantOptionConfigurationuration : IEntityTypeConfiguration<VariantOption>
    {
        public void Configure(EntityTypeBuilder<VariantOption> builder)
        {
            builder.HasKey(x => new { x.VariantId, x.ProductId, x.ProductAttributeId });

            builder.Property(x => x.Value)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(x => x.Variant)
                .WithMany(x => x.VariantOptions)
                .HasForeignKey(x => new { x.VariantId, x.ProductId });

            builder.HasOne(x => x.ProductAttribute)
                .WithMany(x => x.VariantOptions)
                .HasForeignKey(x => x.ProductAttributeId);
        }
    }
}
