namespace Discount.gRPC.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public Guid ProductId { get; set; } = new();
        public string Description { get; set; } = default!;
        public DiscountType DiscountType { get; set; }
        public double Amount { get; set; }
        public double MinSpend { get; set; } = 0;
        public double MaxSpend { get; set; } = double.MaxValue;
        public int UsageLimit { get; set; } = int.MaxValue;
        public int UsageLimitPerUser { get; set; } = 1;
        //public DateTime DateExpire { get; set; }

    }

    public enum DiscountType
    {
        Unknown = 0,
        Percentage = 1,
        FixedProduct = 2
    }
}
