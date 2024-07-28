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
                new Coupon() 
                { 
                    Id = 1, 
                    Code = "ABC123",
                    ProductId = new Guid("6267e3aa-3e20-4600-8cf8-ae908a55eb30"), 
                    Description = "IPhone X 256GB",
                    DiscountType = DiscountType.FixedProduct,
                    Amount = 20000,
                    MinSpend = 20000,
                    UsageLimit = 10,
                    //DateExpire = DateTime.UtcNow.AddDays(10)
                },
                new Coupon() 
                { 
                    Id = 2,
                    Code = "MNPFS",
                    ProductId = new Guid("5853fe3d-677c-4bce-aa27-d12bf2b45e2e"), 
                    Description = "Laptop ASUS Travel Mate RAM 32GB",
                    DiscountType = DiscountType.Percentage,
                    Amount = 20,
                    MinSpend = 10000,
                    UsageLimit = 20,
                    //DateExpire = DateTime.UtcNow.AddDays(7)
                },
                new Coupon() 
                { 
                    Id = 3,
                    Code = "TRTH",
                    ProductId = new Guid("6267e3aa-3e20-4600-8cf8-ae908a55eb30"), 
                    Description = "Samsung Galaxy 256GB",
                    DiscountType = DiscountType.FixedProduct,
                    Amount = 15000,
                    MinSpend = 20000,
                    UsageLimit = 5,
                    //DateExpire = DateTime.UtcNow.AddDays(5)
                }
            );
        }

        public DbSet<Coupon> Coupons { get; set; }
    }
}
