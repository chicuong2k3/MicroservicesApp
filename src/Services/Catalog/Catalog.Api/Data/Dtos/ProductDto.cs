namespace Catalog.Api.Data.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string? Slug { get; set; }
        public string ThumbnailUrl { get; set; } = default!;
        public List<VariantDto> Variants { get; set; } = new();
        public int CategoryId { get; set; }
    }
}
