using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.Update;

public class UpdateProductRequest
{
    public Product Product { get; set; } = default!;
}
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products/{id}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateProductCommand>();
            command.Product.Id = id;
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
