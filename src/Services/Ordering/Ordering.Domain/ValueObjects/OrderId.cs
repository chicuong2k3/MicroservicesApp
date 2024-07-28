namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Value { get; }
        private OrderId(Guid value) => Value = value;
        public static OrderId Generate(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new ArgumentException("OrderId cannot be empty.");
            }

            return new OrderId(value);
        }
    }
}
