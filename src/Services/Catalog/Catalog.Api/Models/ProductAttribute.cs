namespace Catalog.Api.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<VariantOption> VariantOptions { get; set; } = new();
    }
}
