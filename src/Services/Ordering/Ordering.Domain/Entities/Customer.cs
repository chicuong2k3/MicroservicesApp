
namespace Ordering.Domain.Entities
{
    public class Customer : Entity<CustomerId>
    {
        public string UserName { get; private set; } = default!;
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;
        protected Customer() { }
        public static Customer Create(CustomerId customerId, string userName, string firstName, string lastName, string email)
        {
            // Validation

            return new Customer()
            {
                Id = customerId,
                UserName = userName,
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };
        }
    }
}
