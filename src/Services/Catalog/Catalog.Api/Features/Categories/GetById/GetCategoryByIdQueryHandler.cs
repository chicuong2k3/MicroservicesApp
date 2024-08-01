using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Categories.GetById;

public class GetCategoryByIdQuery : IQuery<Category>
{
    public int Id { get; set; }
}

internal class GetCategoryByIdQueryHandler(IDocumentSession session)
    : IQueryHandler<GetCategoryByIdQuery, Category>
{
    public async Task<Category> Handle([FromQuery] GetCategoryByIdQuery query, CancellationToken cancellationToken)
    {
        var category = await session.LoadAsync<Category>(query.Id, cancellationToken);

        if (category == null)
        {
            throw new CategoryNotFoundException(query.Id);
        }

        return category;
    }
}
