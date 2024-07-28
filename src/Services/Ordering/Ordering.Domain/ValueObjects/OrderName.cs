namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        public string Value { get; } = default!;
        private OrderName(string value) => Value = value;
        public static OrderName Generate(string value)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

            return new OrderName(value);
        }
    }
}
