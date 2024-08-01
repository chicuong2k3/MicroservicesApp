
namespace Catalog.Api.Features.Categories.Delete;
public class DeleteCategoryCommand : ICommand
{
    public int Id { get; set; }
}

public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
{
    public DeleteCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
    }
}
internal class DeleteCategoryCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {

        session.Delete<Category>(command.Id);
        await session.SaveChangesAsync();

        return new Unit();
    }
}
