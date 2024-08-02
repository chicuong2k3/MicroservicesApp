
using Catalog.Api.Data;
using Catalog.Api.Data.Dtos;
using Catalog.Api.Extensions;
using Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Features.Products.GetSeveral;

public class GetProductsQuery() : PaginationRequest, IQuery<PaginatedResult<ProductDto>>
{

}

internal class GetProductsQueryHandler(AppDbContext dbContext)
    : IQueryHandler<GetProductsQuery, PaginatedResult<ProductDto>>
{
    public async Task<PaginatedResult<ProductDto>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = dbContext.Products;


        return new PaginatedResult<ProductDto>(
            query.PageNumber,
            query.PageSize,
            products.Count(),
            await products.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize)
                    .Include(x => x.Variants)
                    .ThenInclude(x => x.VariantOptions)
                    .ThenInclude(x => x.ProductAttribute)
                    .Select(x => x.ToProductDto()).ToListAsync()
        );


    }
}
