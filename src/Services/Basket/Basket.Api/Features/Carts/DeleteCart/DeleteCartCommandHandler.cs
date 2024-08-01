
namespace Basket.Api.Features.Carts.DeleteCart;


public class DeleteCartCommand : ICommand
{
    public Guid UserId { get; set; }
}

public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required.");
    }
}
public class DeleteCartCommandHandler(ICartRepository cartRepository)
    : ICommandHandler<DeleteCartCommand>
{
    public async Task<Unit> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        await cartRepository.DeleteCartAsync(command.UserId, cancellationToken);
            
        return new Unit();
    }
}
