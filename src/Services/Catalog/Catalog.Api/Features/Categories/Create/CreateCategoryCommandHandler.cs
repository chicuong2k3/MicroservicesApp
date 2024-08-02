using Catalog.Api.Data;

namespace Catalog.Api.Features.Categories.Create;
public class CreateCategoryCommand : ICommand<Category>
{
    public string Name { get; set; } = default!;
    public string? Slug { get; set; }
    public int? ParentCategoryId { get; set; }
}

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");


    }
}
internal class CreateCategoryCommandHandler(
    AppDbContext dbContext)
    : ICommandHandler<CreateCategoryCommand, Category>
{
    public async Task<Category> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {

        var category = command.Adapt<Category>();

        // generate slug

        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();

        return category;
    }
}
