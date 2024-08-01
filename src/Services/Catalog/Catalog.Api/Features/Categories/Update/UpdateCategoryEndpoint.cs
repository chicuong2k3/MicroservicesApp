using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Categories.Update;
public class UpdateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/categories/{id}", async (int id, [FromBody] UpdateCategoryCommand request, ISender sender) =>
        {
            request.Id = id;
            var response = await sender.Send(request);
            return Results.Ok(response);
        })
        .WithName("UpdateCategory")
        .Produces<Category>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update a categories.");
    }
}
