
namespace Catalog.Api.Features.Products.Update;

public class UpdateProductCommand : ICommand<Product>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public int CategoryId { get; set; }
    public List<Variant> Variants { get; set; } = new();
}
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters..");


    }
}
internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand, Product>
{
    public async Task<Product> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Variants = command.Variants;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return product;
    }
}
