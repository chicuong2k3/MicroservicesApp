using Catalog.Api.Data;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Categories.GetById;

public class GetCategoryByIdQuery : IQuery<Category>
{
    public int Id { get; set; }
}

internal class GetCategoryByIdQueryHandler(AppDbContext dbContext)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle([FromQuery] GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories.FindAsync(query.Id);

        if (category == null)
        {
            throw new CategoryNotFoundException(query.Id);
        }

        return category;
    }
}
