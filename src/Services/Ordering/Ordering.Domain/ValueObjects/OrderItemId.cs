

namespace Ordering.Domain.ValueObjects
{
    public class OrderItemId
    {
        public Guid Value { get; }
        private OrderItemId(Guid value) => Value = value;
        public static OrderItemId Generate(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new ArgumentException("OrderItemId cannot be empty.");
            }

            return new OrderItemId(value);
        }
    }
}
