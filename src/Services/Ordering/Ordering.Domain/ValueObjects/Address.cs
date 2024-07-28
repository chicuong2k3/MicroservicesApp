namespace Ordering.Domain.ValueObjects
{
    public record Address
    {
        public string City { get; } = default!;
        public string District { get; } = default!;
        public string Town { get; } = default!;
        public string? AddressLine { get; }
        protected Address() { }
        private Address(string city, string district, string town, string? addressLine)
        {
            City = city;
            District = district;
            Town = town;
            AddressLine = addressLine;
        }

        public static Address Generate(string city, string district, string town, string? addressLine)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
            ArgumentException.ThrowIfNullOrWhiteSpace(district, nameof(district));
            ArgumentException.ThrowIfNullOrWhiteSpace(town, nameof(town));
            ArgumentException.ThrowIfNullOrWhiteSpace(addressLine, nameof(addressLine));

            return new Address(city, district, town, addressLine);
        }
    }
}
