using Favourite.Api.Data;
using Favourite.Api.Models;

namespace Favourite.Api.Features.FavouriteItems.Create;

public record CreateFavouriteItemResult(Guid Id);

public record CreateFavouriteItemCommand
    (Guid ProductId,
    int VariantId,
    DateTime LikedDate) : ICommand<CreateFavouriteItemResult>;

public class CreateFavouriteItemCommandValidator : AbstractValidator<CreateFavouriteItemCommand>
{
    public CreateFavouriteItemCommandValidator()
    {
        //RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
        //RuleFor(x => x.OrderName).NotEmpty().WithMessage("OrderName is required.");
        //RuleFor(x => x.OrderItems).NotEmpty().WithMessage("OrderItems cannot be empty.");
    }
}
public class CreateFavouriteItemCommandHandler(IFavouriteItemRepository repository)
    : ICommandHandler<CreateFavouriteItemCommand, CreateFavouriteItemResult>
{
    public async Task<CreateFavouriteItemResult> Handle(CreateFavouriteItemCommand command, CancellationToken cancellationToken)
    {
        var item = command.Adapt<FavouriteItem>();
        await repository.CreateItemAsync(item);

        return new CreateFavouriteItemResult(item.Id);
    }
}
