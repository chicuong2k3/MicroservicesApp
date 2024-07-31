using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.Create;

public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async ([FromBody] CreateProductCommand request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Created($"api/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResult>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create new product.");
    }
}
