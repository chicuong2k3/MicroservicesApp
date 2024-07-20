namespace Catalog.Api.Features.Products.Create;

public class CreateProductResult
{
    public int Id { get; set; }
}
public class CreateProductCommand : ICommand<CreateProductResult>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    //public string FileUrl { get; set; } = default!;
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters.");


        //RuleFor(x => x.FileUrl).NotEmpty().WithMessage("FileUrl is required.")
        //    .MaximumLength(1024).WithMessage("FileUrl must have less than 1024 characters.");

    }
}
internal class CreateProductCommandHandler(
    IDocumentSession session)
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description
        };

        session.Store(product);
        await session.SaveChangesAsync();

        return new CreateProductResult() { Id = product.Id };
    }
}
