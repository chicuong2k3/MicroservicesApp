using Ordering.Domain.Entities;

namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; }
        private CustomerId(Guid value) => Value = value;
        public static CustomerId Generate(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value, nameof(value));
            if (value == Guid.Empty)
            {
                throw new ArgumentException("CustomerId cannot be empty.");
            }

            return new CustomerId(value);
        }
    }
}
