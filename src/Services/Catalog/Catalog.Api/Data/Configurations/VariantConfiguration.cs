using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.Configurations
{
    public class VariantConfiguration : IEntityTypeConfiguration<Variant>
    {
        public void Configure(EntityTypeBuilder<Variant> builder)
        {

            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasKey(x => new { x.Id, x.ProductId });

            builder.Property(x => x.Sku)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(x => x.Product)
                .WithMany(x => x.Variants)
                .HasForeignKey(x => x.ProductId);


        }
    }
}
