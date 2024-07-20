namespace Basket.Api.Models
{
    public class CartItem
    {
        public string SkuId { get; set; } = default!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
