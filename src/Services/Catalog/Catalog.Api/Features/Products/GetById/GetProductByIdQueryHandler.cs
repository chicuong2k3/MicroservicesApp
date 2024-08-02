using Catalog.Api.Data;
using Catalog.Api.Data.Dtos;
using Catalog.Api.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Features.Products.GetById;

public class GetProductByIdQuery : IQuery<ProductDto>
{
    public Guid Id { get; set; }
}

internal class GetProductByIdQueryHandler(AppDbContext dbContext)
    : IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle([FromQuery] GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .Where(x => x.Id == query.Id)
            .Include(x => x.Variants)
            .ThenInclude(x => x.VariantOptions).ThenInclude(x => x.ProductAttribute)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return product.ToProductDto();
    }
}
