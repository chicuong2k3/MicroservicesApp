using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.GetById;

public class GetProductByIdResult
{
    public Product Product { get; set; } = new();
}
public class GetProductByIdQuery : IQuery<GetProductByIdResult>
{
    public Guid Id { get; set; }
}

internal class GetProductByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle([FromQuery] GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var document = await session.LoadAsync<Product>(query.Id, cancellationToken);

        if (document == null)
        {
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult()
        {
            Product = document
        };
    }
}
