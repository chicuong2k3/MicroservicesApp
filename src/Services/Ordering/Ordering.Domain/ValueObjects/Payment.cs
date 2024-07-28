namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        
        public string Title { get; } = default!;
        public int PaymentMethod { get; }
        public DateTime DatePaid { get; }

        protected Payment() { }
        private Payment(string title, int paymentMethod)
        {
            Title = title;
            PaymentMethod = paymentMethod;
            DatePaid = DateTime.UtcNow;
        }
        public static Payment Generate(string title, int paymentMethod)
        {
            // Validation

            return new Payment(title, paymentMethod);
        }

    }
}
