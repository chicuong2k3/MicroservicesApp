namespace Catalog.Api.Models
{
    public class ProductAttributeSku
    {
        public int AttributeId { get; set; }
        public string SkuId { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
