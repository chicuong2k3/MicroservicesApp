

namespace Ordering.Application.Features.Orders.Commands;

public class CreateOrderResult
{
    public Guid Id { get; set; }
}


public record CreateOrderCommand(
    Guid CustomerId,
    string OrderName,
    CreateAddressDto ShippingAddress,
    CreatePaymentDto Payment,
    List<CreateOrderItemDto> OrderItems) : ICommand<CreateOrderResult>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.OrderName).NotEmpty().WithMessage("OrderName is required.");
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("OrderItems cannot be empty.");
    }
}
public class CreateOrderCommandHandler(IAppDbContext dbContext) : 
    ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {

        var order = CreateOrder(command);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult()
        {
            Id = order.Id.Value
        };
    }

    private Order CreateOrder(CreateOrderCommand command)
    {
        var shippingAddress = Address.Generate(
            command.ShippingAddress.City,
            command.ShippingAddress.District,
            command.ShippingAddress.Town,
            command.ShippingAddress.AddressLine);

        var payment = Payment.Generate(
            command.Payment.Title,
            command.Payment.PaymentMethod);

        var order = Order.Create(
            OrderId.Generate(Guid.NewGuid()),
            CustomerId.Generate(command.CustomerId),
            OrderName.Generate(command.OrderName),
            shippingAddress,
            payment);

        foreach (var orderItem in command.OrderItems)
        {
            order.AddOrderItem(
                ProductId.Generate(orderItem.ProductId),
                orderItem.ProductVariantId,
                orderItem.Price,
                orderItem.Quantity);
        }

        return order;
    }
}
