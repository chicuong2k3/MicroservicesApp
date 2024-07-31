
using Common.Messaging.IntegrationEvents;
using MassTransit;

namespace Basket.Api.Features.CheckoutCart;


public class CheckoutCartCommand : ICommand
{
    // Customer's Info
    public string UserName { get; set; } = default!;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    // Shipping Address
    public string City { get; set; } = default!;
    public string District { get; set; } = default!;
    public string Town { get; set; } = default!;
    public string? AddressLine { get; set; }

    // Payment
    public string PaymentTitle { get; set; } = default!;
    public int PaymentMethod { get; set; }
}

public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
{
    public CheckoutCartCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName is required.");
    }
}
public class CheckoutCartCommandHandler(ICartRepository cartRepository, IPublishEndpoint publishEndpoint) 
    : ICommandHandler<CheckoutCartCommand>
{
    public async Task<Unit> Handle(CheckoutCartCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(command.UserName, cancellationToken);

        var eventMessage = command.Adapt<CartCheckoutEvent>();
        eventMessage.CartItems = cart.CartItems.Adapt<List<Common.Messaging.IntegrationEvents.CartItem>>();
        eventMessage.TotalPrice = cart.TotalPrice;

        await publishEndpoint.Publish(eventMessage, cancellationToken);

        await cartRepository.DeleteCartAsync(command.UserName, cancellationToken);

        return new Unit();
    }
}
