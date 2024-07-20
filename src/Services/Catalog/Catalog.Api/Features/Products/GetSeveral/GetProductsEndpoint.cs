
namespace Catalog.Api.Features.Products.GetSeveral;

public class GetProductsRequest
{
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}
public class GetProductsResponse
{
    public IEnumerable<Product> Products { get; set; } = new List<Product>();
}
public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();

            var result = await sender.Send(query);
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProducts")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get several products.");
    }
}
