using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.Update;

public class UpdateProductRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    //public string FileUrl { get; set; } = default!;
}
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products/{id}", async (int id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            command.Id = id;
            await sender.Send(command);
            return Results.Ok("Product updated.");
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update a product.");
    }
}
