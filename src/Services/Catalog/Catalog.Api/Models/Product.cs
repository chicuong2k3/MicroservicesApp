namespace Catalog.Api.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public int CategoryId { get; set; }
        public List<Variant> Variants { get; set; } = new();
    }
}
