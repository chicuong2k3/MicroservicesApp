
namespace Basket.Api.Features.Carts.GetCart;

public class GetCartResponse
{
    public Cart Cart { get; set; } = new();
}

public class GetCartQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/carts/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetCartQuery() { UserName = userName });
            var response = result.Adapt<GetCartResponse>();
            return Results.Created($"api/carts/{response.Cart.UserName}", response);
        })
        .WithName("GetCart")
        .Produces<Cart>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get user's cart.");
    }
}
