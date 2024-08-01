

namespace Catalog.Api.Features.Categories.GetById;

public class GetCategoryByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories/{id}", async (int id, ISender sender) =>
        {
            var response = await sender.Send(new GetCategoryByIdQuery() { Id = id });
            return Results.Ok(response);
        })
        .WithName("GetCategoryById")
        .Produces<Category>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get category by id.");
    }
}
