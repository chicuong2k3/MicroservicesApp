


namespace Ordering.Domain.Entities
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems { get => orderItems.AsReadOnly(); }
        public CustomerId CustomerId { get; private set; } = default!;
        public OrderName OrderName { get; private set; } = default!;

        public Address ShippingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        //public double TotalDiscount { get; set; }
        //public double TotalShipping { get; set; }
        //public double GrandTotal { get; set; }

        public double TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        protected Order()
        {
            
        }

        public static Order Create(
            OrderId orderId, 
            CustomerId customerId, 
            OrderName orderName, 
            Address shippingAddress, 
            Payment payment)
        {


            var order = new Order()
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                Payment = payment,
                Status = OrderStatus.Pending
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }
        public void Update(
            OrderName orderName,
            Address shippingAddress,
            Payment payment,
            OrderStatus status)
        {

            OrderName = orderName;
            ShippingAddress = shippingAddress;
            Payment = payment;
            Status = status;

            AddDomainEvent(new OrderUpdatedEvent(this));

        }

        public void AddOrderItem(ProductId productId, int productVariantId, double price, int quantity)
        {
            orderItems.Add(new OrderItem(Id, productId, productVariantId, price, quantity));
        }
        public void RemoveOrderItem(OrderItemId orderItemId)
        {
            var orderItem = orderItems.FirstOrDefault(x => x.Id == orderItemId);

            if (orderItem != null)
            {
                orderItems.Remove(orderItem);
            }
            
        }
    }
}
