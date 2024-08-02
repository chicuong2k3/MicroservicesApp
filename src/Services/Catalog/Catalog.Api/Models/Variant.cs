using System.Text.Json.Serialization;

namespace Catalog.Api.Models
{
    public class Variant
    {
        public int Id { get; set; }
        public Guid ProductId { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }
        public string Sku { get; set; } = default!;
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<VariantOption> VariantOptions { get; set; } = new();
    }
}
