using Discount.gRPC.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon() { Id = 1, SkuId = "abc123", Description = "IPhone X 256GB", Percent = 15 },
                new Coupon() { Id = 2, SkuId = "mnp456", Description = "Laptop ASUS Travel Mate RAM 32GB", Percent = 20 },
                new Coupon() { Id = 3, SkuId = "xyz789", Description = "Samsung Galaxy 256GB", Percent = 15 }
            );
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
