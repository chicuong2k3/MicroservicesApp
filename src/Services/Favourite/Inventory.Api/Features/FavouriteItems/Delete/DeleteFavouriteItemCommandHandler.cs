namespace Favourite.Api.Features.FavouriteItems.Delete;

public record DeleteFavouriteItemCommand(Guid Id) : ICommand;

public class DeleteFavouriteItemCommandValidator : AbstractValidator<DeleteFavouriteItemCommand>
{
    public DeleteFavouriteItemCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
    }
}
public class DeleteFavouriteItemCommandHandler(IFavouriteItemRepository repository)
    : ICommandHandler<DeleteFavouriteItemCommand>
{
    public async Task<Unit> Handle(DeleteFavouriteItemCommand command, CancellationToken cancellationToken)
    {
        await repository.DeleteItemAsync(command.Id);

        return new Unit();
    }
}
