namespace Catalog.Api.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Slug { get; set; }
        public int? ParentCategoryId { get; set; }

    }
}
