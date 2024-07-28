
namespace Catalog.Api.Features.Products.Delete;
public class DeleteProductCommand : ICommand
{
    public Guid Id { get; set; }
}

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");
    }
}
internal class DeleteProductCommandHandler(IDocumentSession session)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {

        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync();

        return new Unit();
    }
}
