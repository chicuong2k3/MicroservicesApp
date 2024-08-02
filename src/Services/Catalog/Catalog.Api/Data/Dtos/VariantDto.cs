namespace Catalog.Api.Data.Dtos
{
    public class VariantDto
    {
        public string Sku { get; set; } = default!;
        public int StockQuantity { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        public List<AttributeDto> Attributes { get; set; } = new();
    }
}
