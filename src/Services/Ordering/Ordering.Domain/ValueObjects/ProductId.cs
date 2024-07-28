

namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Value { get; }
        private ProductId(Guid value) => Value = value;
        public static ProductId Generate(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new ArgumentException("ProductId cannot be empty.");
            }

            return new ProductId(value);
        }
    }
}
