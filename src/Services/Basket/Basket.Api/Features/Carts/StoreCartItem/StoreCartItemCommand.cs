
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
        var cart = command.Cart;

        foreach (var item in command.Cart.CartItems)
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

        var cartResult = await cartRepository.StoreCartItemAsync(cart, cancellationToken);
        return new StoreCartItemResult()
        {
            UserName = cartResult.UserName
        };
    }
}
