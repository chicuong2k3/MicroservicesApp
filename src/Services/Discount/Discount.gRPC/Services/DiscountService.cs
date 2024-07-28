using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Discount.gRPC.Protos;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountContext> logger)
        : DiscountProto.DiscountProtoBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductId.ToString().ToLower() == request.ProductId.ToLower());

            if (coupon == null)
            {
                return new CouponModel
                {
                    ProductId = "No Discount",
                    Description = "No Description",
                    DiscountType = Protos.DiscountType.Unknown,
                    Amount = 0,
                    MinSpend = 0,
                    MaxSpend = 0
                };
            }

            logger.LogInformation("Discount is retrieve for proudct: {productId}.", request.ProductId);

            return coupon.Adapt<CouponModel>();
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>();

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is created for product: {productId}.", coupon.ProductId);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductId.ToString().ToLower() == request.ProductId.ToLower());

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            coupon.Description = request.Description;
            coupon.DiscountType = (Models.DiscountType)request.DiscountType;    
            coupon.Amount = request.Amount;
            coupon.MinSpend = request.MinSpend;
            coupon.MaxSpend = request.MaxSpend;
            coupon.UsageLimit = request.UsageLimit;
            coupon.UsageLimitPerUser = request.UsageLimitPerUser;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is updated for product: {productId}.", coupon.ProductId);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.ProductId.ToString().ToLower() == request.ProductId.ToLower());

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is deleted for product: {productId}.", coupon.ProductId);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
