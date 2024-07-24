
using Discount.gRPC.Protos;

namespace Basket.Api.Features.Carts.StoreCartItem;

public class StoreCartItemResult
{
    public string UserName { get; set; } = default!;
}
public class StoreCartItemCommand : ICommand<StoreCartItemResult>
{
    public Cart Cart { get; set; } = default!;
}

public class StoreCartItemCommandValidator : AbstractValidator<StoreCartItemCommand>
{
    public StoreCartItemCommandValidator()
    {
        RuleFor(x => x.Cart)
            .NotNull().WithMessage("Cart is required.");

        RuleFor(x => x.Cart.UserName)
                    .NotEmpty().WithMessage("UserName is required.");

        RuleForEach(x => x.Cart.CartItems)
            .SetValidator(new CartItemValidator());
    }
}

public class CartItemValidator : AbstractValidator<CartItem>
{
    public CartItemValidator()
    {
        RuleFor(x => x.SkuId)
            .NotEmpty().WithMessage("SkuId is required.");

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
        var cart = command.Cart;

        foreach (var item in command.Cart.CartItems)
        {
            var coupon = await discountClient.GetDiscountAsync(new GetDiscountRequest()
            {
                SkuId = item.SkuId
            }, cancellationToken: cancellationToken);

            item.Price -= (decimal)coupon.Percent / 100 * item.Price;    
        }

        var cartResult = await cartRepository.StoreCartItemAsync(cart, cancellationToken);
        return new StoreCartItemResult()
        {
            UserName = cartResult.UserName
        };
    }
}
