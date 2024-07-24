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
                .FirstOrDefaultAsync(x => x.SkuId == request.SkuId);

            if (coupon == null)
            {
                return new CouponModel
                {
                    SkuId = "No Discount",
                    Description = "No Description",
                    Percent = 0
                };
            }

            logger.LogInformation("Discount is retrieve for proudct with sku: {skuId}.", request.SkuId);

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

            logger.LogInformation("Discount is created for product with sku: {skuId}.", coupon.SkuId);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.SkuId == request.SkuId);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            coupon.Description = request.Description;
            coupon.Percent = request.Percent;

            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is updated for product with sku: {skuId}.", coupon.SkuId);

            return coupon.Adapt<CouponModel>();
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons
                .FirstOrDefaultAsync(x => x.SkuId == request.SkuId);

            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object."));
            }

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is deleted for product with sku: {skuId}.", coupon.SkuId);

            return new DeleteDiscountResponse { Success = true };
        }
    }
}
