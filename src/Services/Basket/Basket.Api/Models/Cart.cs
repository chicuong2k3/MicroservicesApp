namespace Basket.Api.Models
{
    public class Cart
    {
        public string UserName { get; set; } = default!;
        public List<CartItem> CartItems { get; set; } = new();
        public decimal TotalPrice { get => CartItems.Sum(x => x.Price * x.Quantity); }
    }
}
