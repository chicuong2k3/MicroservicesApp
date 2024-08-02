using Catalog.Api.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Features.Products.Update;
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/products/{id}", async (Guid id, [FromBody] UpdateProductCommand request, ISender sender) =>
        {
            request.Id = id;
            var response = await sender.Send(request);
            return Results.Ok(response);
        })
        .WithName("UpdateProduct")
        .Produces<ProductDto>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update a product.");
    }
}
