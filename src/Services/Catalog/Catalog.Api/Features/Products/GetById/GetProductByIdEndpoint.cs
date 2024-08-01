

namespace Catalog.Api.Features.Products.GetById;

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{id}", async (Guid id, ISender sender) =>
        {
            var response = await sender.Send(new GetProductByIdQuery() { Id = id });
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get product by id.");
    }
}
