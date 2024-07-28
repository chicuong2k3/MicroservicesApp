namespace Ordering.Domain.Entities
{
    public class OrderItem : Entity<OrderItemId>
    {
        public OrderId OrderId { get; private set; } = default!;
        public ProductId ProductId { get; private set; } = default!;
        public int ProductVariantId { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public double Total { get => Price * Quantity; }

        internal OrderItem(OrderId orderId, ProductId productId, int productVariantId, double price, int quantity)
        {
            Id = OrderItemId.Generate(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            ProductVariantId = productVariantId;
            Price = price;
            Quantity = quantity;
        }
    }
}
