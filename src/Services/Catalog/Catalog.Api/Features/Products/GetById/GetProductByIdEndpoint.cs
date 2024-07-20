

namespace Catalog.Api.Features.Products.GetById;

public class GetProductByIdResponse
{
    public Product Product { get; set; } = new();
}
public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products/{id}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery() { Id = id });
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get product by id.");
    }
}
