using System.Text.Json.Serialization;

namespace Catalog.Api.Models
{
    public class VariantOption
    {
        public int VariantId { get; set; }
        [JsonIgnore]
        public Variant? Variant { get; set; }
        public Guid ProductId { get; set; }
        public int ProductAttributeId { get; set; }

        public ProductAttribute? ProductAttribute { get; set; }
        public string Value { get; set; } = default!;
    }
}
