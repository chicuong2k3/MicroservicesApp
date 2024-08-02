
using Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

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
internal class DeleteProductCommandHandler(AppDbContext dbContext)
    : ICommandHandler<DeleteProductCommand>
{
    public async Task<Unit> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {

        var product = await dbContext.Products.FindAsync(command.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();

        return new Unit();
    }
}
