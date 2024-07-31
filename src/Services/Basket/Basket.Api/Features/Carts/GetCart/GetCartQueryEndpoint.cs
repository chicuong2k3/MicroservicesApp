
namespace Basket.Api.Features.Carts.GetCart;

public class GetCartQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/carts/{userName}", async (string userName, ISender sender) =>
        {
            var response = await sender.Send(new GetCartQuery() { UserName = userName });
            return Results.Created($"api/carts/{response.Cart.UserName}", response);
        })
        .WithName("GetCart")
        .Produces<GetCartResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get user's cart.");
    }
}
