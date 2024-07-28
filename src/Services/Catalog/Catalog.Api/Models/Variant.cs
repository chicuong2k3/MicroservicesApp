namespace Catalog.Api.Models
{
    public class Variant
    {
        public int Id { get; set; }
        public string Sku { get; set; } = default!;
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public List<VariantOption> Options { get; set; } = new();
    }
}
