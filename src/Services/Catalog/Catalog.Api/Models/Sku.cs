namespace Catalog.Api.Models
{
    public class Sku
    {
        public string Id { get; set; } = default!;
        public int ProductId { get; set; }
        public string CurrencyCode { get; set; } = default!;
        public int AmountOfStock { get; set; }
        public decimal Price { get; set; }
    }
}
