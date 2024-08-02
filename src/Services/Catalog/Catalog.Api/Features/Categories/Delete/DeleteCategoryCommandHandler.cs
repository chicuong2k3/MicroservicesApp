
using Catalog.Api.Data;

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
internal class DeleteCategoryCommandHandler(AppDbContext dbContext)
    : ICommandHandler<DeleteCategoryCommand>
{
    public async Task<Unit> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
    {

        var category = await dbContext.Categories.FindAsync(command.Id);

        if (category == null)
        {
            throw new CategoryNotFoundException(command.Id);
        }

        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();

        return new Unit();
    }
}
