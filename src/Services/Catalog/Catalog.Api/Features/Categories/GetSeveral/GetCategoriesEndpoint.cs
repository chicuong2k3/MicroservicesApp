
namespace Catalog.Api.Features.Categories.GetSeveral;

public class GetCategoriesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/categories", async ([AsParameters] GetCategoriesQuery request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Ok(response);
        })
        .WithName("GetCategories")
        .Produces<GetCategoriesResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get several categories.");
    }
}
