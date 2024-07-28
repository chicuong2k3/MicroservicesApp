namespace Basket.Api.Models
{
    public class CartItem
    {
        public Guid ProductId { get; set; }
        public int VariantId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
