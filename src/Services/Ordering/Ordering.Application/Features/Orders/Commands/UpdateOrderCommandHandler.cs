


using Ordering.Domain.Enums;

namespace Ordering.Application.Features.Orders.Commands;

public record UpdateOrderCommand(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    UpdateAddressDto ShippingAddress,
    UpdatePaymentDto Payment,
    List<UpdateOrderItemDto> OrderItems,
    OrderStatus Status) : ICommand;
public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.OrderName).NotEmpty().WithMessage("OrderName is required.");
    }
}
public class UpdateOrderCommandHandler(IAppDbContext dbContext) : 
    ICommandHandler<UpdateOrderCommand>
{
    public async Task<Unit> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {

        var orderId = OrderId.Generate(command.Id);
        var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException(orderId.Value);
        }

        UpdateOrder(order, command);

        dbContext.Orders.Update(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new Unit();
    }


    private void UpdateOrder(Order order, UpdateOrderCommand command)
    {
        var shippingAddress = Address.Generate(
            command.ShippingAddress.City,
            command.ShippingAddress.District,
            command.ShippingAddress.Town,
            command.ShippingAddress.AddressLine);

        var payment = Payment.Generate(
            command.Payment.Title,
            command.Payment.PaymentMethod);

        order.Update(
            OrderName.Generate(command.OrderName),
            shippingAddress,
            payment,
            command.Status);
    }
}
