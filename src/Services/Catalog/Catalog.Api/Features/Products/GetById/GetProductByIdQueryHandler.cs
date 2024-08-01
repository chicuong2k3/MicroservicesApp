using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.GetById;

public class GetProductByIdQuery : IQuery<Product>
{
    public Guid Id { get; set; }
}

internal class GetProductByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, Product>
{
    public async Task<Product> Handle([FromQuery] GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return product;
    }
}
