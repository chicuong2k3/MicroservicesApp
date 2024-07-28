namespace Catalog.Api.Features.Products.Create;

public class CreateProductResult
{
    public Guid Id { get; set; }
}
public class CreateProductCommand : ICommand<CreateProductResult>
{
    public Product Product { get; set; } = default!;
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Product.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters.");


        

    }
}
internal class CreateProductCommandHandler(
    IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var product = command.Product.Adapt<Product>(); 

        session.Store(product);
        await session.SaveChangesAsync();

        return new CreateProductResult() { Id = product.Id };
    }
}
