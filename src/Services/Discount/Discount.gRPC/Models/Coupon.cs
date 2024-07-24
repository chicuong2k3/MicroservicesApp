namespace Discount.gRPC.Models
{
    public class Coupon
    {
        public int Id { get; set; }
        public string SkuId { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Percent { get; set; }
    }
}
