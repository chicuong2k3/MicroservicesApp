using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Categories.Create;

public class CreateCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/categories", async ([FromBody] CreateCategoryCommand request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Created($"api/categories/{response.Id}", response);
        })
        .WithName("CreateCategory")
        .Produces<Category>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create new category.");
    }
}
