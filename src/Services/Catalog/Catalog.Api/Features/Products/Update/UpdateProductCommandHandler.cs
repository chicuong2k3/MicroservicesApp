
namespace Catalog.Api.Features.Products.Update;

public class UpdateProductCommand : ICommand
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    //public string FileUrl { get; set; } = default!;
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

        //RuleFor(x => x.FileUrl).NotEmpty().WithMessage("FileUrl is required.")
        //    .MaximumLength(1024).WithMessage("FileUrl must have less than 1024 characters.");

    }
}
internal class UpdateProductCommandHandler(IDocumentSession session)
    : ICommandHandler<UpdateProductCommand>
{
    public async Task<Unit> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;

        //if (!string.IsNullOrEmpty(command.FileUrl))
        //{
        //    product.FileUrl = command.FileUrl;
        //}

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
