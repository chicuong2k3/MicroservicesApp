


namespace Ordering.Application.Features.Orders.Commands;

public class DeleteOrderResult
{
    public bool IsSuccess { get; set; }
}

public class DeleteOrderCommand : ICommand<DeleteOrderResult>
{
    public Guid OrderId { get; set; } = default!;
}

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
    }
}
public class DeleteOrderCommandHandler(IAppDbContext dbContext) : 
    ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {

        var orderId = OrderId.Generate(command.OrderId);
        var order = await dbContext.Orders.FindAsync(orderId, cancellationToken);

        if (order == null)
        {
            throw new OrderNotFoundException(orderId.Value);
        } 

        dbContext.Orders.Remove(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult()
        {
            IsSuccess = true
        };
    }

}
