namespace Catalog.Api.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = default!;
    }
}
