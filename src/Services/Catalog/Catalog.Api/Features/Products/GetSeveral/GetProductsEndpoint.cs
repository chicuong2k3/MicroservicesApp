
namespace Catalog.Api.Features.Products.GetSeveral;

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", async ([AsParameters] GetProductsQuery request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get several products.");
    }
}
