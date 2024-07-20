
namespace Basket.Api.Features.Carts.DeleteCart;


public class DeleteCartCommand : ICommand
{
    public string UserName { get; set; } = default!;
}

public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
{
    public DeleteCartCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
    }
}
public class DeleteCartCommandHandler(ICartRepository cartRepository)
    : ICommandHandler<DeleteCartCommand>
{
    public async Task<Unit> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
    {
        await cartRepository.DeleteCartAsync(command.UserName, cancellationToken);
            
        return new Unit();
    }
}
