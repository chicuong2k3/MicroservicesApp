
using Discount.gRPC.Protos;

namespace Basket.Api.Features.Carts.StoreCartItem;

public class StoreCartItemResult
{
    public Guid UserId { get; set; } 
}
public class StoreCartItemCommand : ICommand<StoreCartItemResult>
{
    public Guid UserId { get; set; }
    public List<CartItem> CartItems { get; set; } = new();
}

public class StoreCartItemCommandValidator : AbstractValidator<StoreCartItemCommand>
{
    public StoreCartItemCommandValidator()
    {

        RuleFor(x => x.UserId)
                    .NotEmpty().WithMessage("UserId is required.");

        RuleForEach(x => x.CartItems)
            .SetValidator(new CartItemValidator());
    }
}

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotNull().WithMessage("ProductId is required.");

        RuleFor(x => x.VariantId)
            .NotNull().WithMessage("VariantId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
    }
}

public class StoreCartItemCommandHandler(ICartRepository cartRepository, DiscountProto.DiscountProtoClient discountClient)
    : ICommandHandler<StoreCartItemCommand, StoreCartItemResult>
{
    public async Task<StoreCartItemResult> Handle(StoreCartItemCommand command, CancellationToken cancellationToken)
    {
        
        foreach (var item in command.CartItems)
        {
            var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest()
            {
                ProductId = item.ProductId.ToString()
            }, cancellationToken: cancellationToken);

            if (coupon.DiscountType == DiscountType.Percentage)
            {
                item.Price -= (double)coupon.Amount / 100 * item.Price;
            }    
            else if (coupon.DiscountType == DiscountType.FixedProduct)
            {
                item.Price -= coupon.Amount;
            }
        }

        var cart = command.Adapt<Cart>();

        var cartResult = await cartRepository.StoreCartItemAsync(cart, cancellationToken);
        return new StoreCartItemResult()
        {
            UserId = cartResult.UserId
        };
    }
}
