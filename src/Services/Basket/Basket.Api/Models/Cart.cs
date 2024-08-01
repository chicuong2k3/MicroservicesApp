namespace Basket.Api.Models
{
    public class Cart
    {
        public Guid UserId { get; set; }
        public List<CartItem> CartItems { get; set; } = new();
        public double TotalPrice { get => CartItems.Sum(x => x.Price * x.Quantity); }
    }
}
