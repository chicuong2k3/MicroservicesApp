namespace Catalog.Api.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public List<Sku> Skus { get; set; } = new();
    }
}
