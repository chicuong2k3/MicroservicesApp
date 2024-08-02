
using Catalog.Api.Data;

namespace Catalog.Api.Features.Categories.Update;

public class UpdateCategoryCommand : ICommand<Category>
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public int? ParentCategoryId { get; set; }
}
public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");



    }
}
internal class UpdateCategoryCommandHandler(AppDbContext dbContext)
    : ICommandHandler<UpdateCategoryCommand, Category>
{
    public async Task<Category> Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync(command.Id);

        if (category == null)
        {
            throw new CategoryNotFoundException(command.Id);
        }

        category.Name = command.Name;

        if (command.Slug != null)
        {
            category.Slug = command.Slug;
        }

        if (command.ParentCategoryId != null)
        {
            category.ParentCategoryId = command.ParentCategoryId;
        }


        await dbContext.SaveChangesAsync();

        return category;
    }
}
