
namespace Catalog.Api.Features.Products.Update;

public class UpdateProductCommand : ICommand
{
    public Product Product { get; set; } = default!;
}
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotNull().WithMessage("Id is required.");

        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters..");


    }
}
internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Product.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Product.Id);
        }

        product.Name = command.Product.Name;
        product.Description = command.Product.Description;
        product.Variants = command.Product.Variants;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
