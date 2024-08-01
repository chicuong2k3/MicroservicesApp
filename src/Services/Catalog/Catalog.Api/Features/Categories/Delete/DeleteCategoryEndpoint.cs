
namespace Catalog.Api.Features.Categories.Delete;

public class DeleteCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/categories/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteCategoryCommand() { Id = id });
            return Results.NoContent();
        })
        .WithName("DeleteCategory")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete category.");
    }
}
