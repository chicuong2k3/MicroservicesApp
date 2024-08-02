
using Catalog.Api.Data;
using Catalog.Api.Data.Dtos;
using Catalog.Api.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Features.Products.Create;

public class CreateProductCommand : ICommand<ProductDto>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Slug { get; set; }
    public string ThumbnailUrl { get; set; } = default!;
    public List<VariantDto> Variants { get; set; } = new();
    public int? CategoryId { get; set; }
}

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.ThumbnailUrl).NotEmpty().WithMessage("ThumbnailUrl is required.")
            .MaximumLength(1024).WithMessage("ThumbnailUrl must less than 1024 characters.");

    }
}
internal class CreateProductCommandHandler(
    AppDbContext dbContext)
    : ICommandHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {

        var product = command.Adapt<Product>();

        var variants = new List<Variant>();


        foreach (var variantDto in command.Variants)
        {
            var variant = variantDto.Adapt<Variant>();
            var variantOptions = new List<VariantOption>();

            foreach (var attr in variantDto.Attributes)
            { 
                var attribute = await dbContext.ProductAttributes
                        .FirstOrDefaultAsync(x => x.Name == attr.Name);

                if (attribute == null)
                {
                    attribute = new ProductAttribute() { Name = attr.Name };
                    dbContext.ProductAttributes.Add(attribute);
                    await dbContext.SaveChangesAsync();
                }

                variantOptions.Add(new VariantOption()
                {
                    ProductAttribute = attribute,
                    Value = attr.Value
                });
            }

            variant.VariantOptions = variantOptions;

            variants.Add(variant);
        }

        product.Variants = variants;
        dbContext.Products.Add(product);

        await dbContext.SaveChangesAsync();

        return product.ToProductDto();
    }
}
