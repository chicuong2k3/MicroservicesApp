namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
        public decimal OriginalPrice { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Categories { get; set; } = new();
    }
}
