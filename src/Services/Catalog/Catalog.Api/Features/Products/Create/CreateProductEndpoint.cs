using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.Create;

public class CreateProductRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    //public string FileUrl { get; set; } = default!;
}
public class CreateProductResponse
{
    public int Id { get; set; }
}
public class CreateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/products", async ([FromBody] CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateProductResponse>();
            return Results.Created($"api/products/{response.Id}", response);
        })
        .WithName("CreateProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create new product.");
    }
}
