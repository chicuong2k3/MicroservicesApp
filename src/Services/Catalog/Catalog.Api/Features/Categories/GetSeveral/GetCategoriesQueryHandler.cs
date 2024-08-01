

namespace Catalog.Api.Features.Categories.GetSeveral;

public class GetCategoriesResult
{
    public IEnumerable<Category> Categories { get; set; } = new List<Category>();
}
public class GetCategoriesQuery : IQuery<GetCategoriesResult>
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

internal class GetCategoriesQueryHandler(IDocumentSession session)
    : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
{
    public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Category> categories = session.Query<Category>();


        return new GetCategoriesResult()
        {
            Categories = await categories.ToPagedListAsync(query?.PageNumber ?? 1, query?.PageSize ?? 10, cancellationToken)

        };


    }
}
