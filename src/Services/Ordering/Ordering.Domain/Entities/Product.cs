
namespace Ordering.Domain.Entities
{
    public class Product : Entity<ProductId>
    {
        public string Name { get; private set; } = default!;
        public double Price { get; private set; }

        protected Product() { }
        public static Product Create(ProductId productId, string name, double price) 
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentOutOfRangeException.ThrowIfNegative(price);

            return new Product()
            {
                Id = productId,
                Name = name,
                Price = price
            };
        }
    }
}
