namespace Catalog.Api.Features.Products.Create;

public class CreateProductCommand : ICommand<Product>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public int CategoryId { get; set; }
    public List<Variant> Variants { get; set; } = new();
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters.");


        

    }
}
internal class CreateProductCommandHandler(
    IDocumentSession session)
    : ICommandHandler<CreateProductCommand, Product>
{
    public async Task<Product> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var product = command.Adapt<Product>(); 

        session.Store(product);
        await session.SaveChangesAsync();

        return product;
    }
}
