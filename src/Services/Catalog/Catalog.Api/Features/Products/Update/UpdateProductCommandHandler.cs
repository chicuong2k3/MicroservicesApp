
using Catalog.Api.Data;
using Catalog.Api.Data.Dtos;
using Catalog.Api.Extensions;

namespace Catalog.Api.Features.Products.Update;

public class UpdateProductCommand : ICommand<ProductDto>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string? ThumbnailUrl { get; set; } = default!;
    public int? CategoryId { get; set; }
}
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id is required.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.ThumbnailUrl).NotEmpty().WithMessage("ThumbnailUrl is required.")
            .MaximumLength(1024).WithMessage("ThumbnailUrl must less than 1024 characters.");


    }
}
internal class UpdateProductCommandHandler(AppDbContext dbContext)
    : ICommandHandler<UpdateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products.FindAsync(command.Id);

        if (product == null)
        {
            throw new ProductNotFoundException(command.Id);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Slug = command.Slug;

        if (command.CategoryId != null)
        {
            product.CategoryId = command.CategoryId.Value;
        }
        
        if (command.ThumbnailUrl != null)
        {
            product.ThumbnailUrl = command.ThumbnailUrl;
        }

        await dbContext.SaveChangesAsync();
        return product.ToProductDto();
    }
}
