
namespace Catalog.Api.Features.Products.GetSeveral;

public class GetProductsResult
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}
public class GetProductsQuery : IQuery<GetProductsResult>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

internal class GetProductsQueryHandler(IDocumentSession session)
    : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Product> products = session.Query<Product>();


        return new GetProductsResult()
        {
            Products = await products.ToPagedListAsync(query?.PageNumber ?? 1, query?.PageSize ?? 10, cancellationToken)
        };


    }
}
