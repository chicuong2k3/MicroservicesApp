

using Catalog.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Features.Categories.GetSeveral;

public class GetCategoriesQuery : IQuery<IEnumerable<Category>>
{
}

internal class GetCategoriesQueryHandler(AppDbContext dbContext)
    : IQueryHandler<GetCategoriesQuery, IEnumerable<Category>>
{
    public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await dbContext.Categories.ToListAsync();


    }
}
