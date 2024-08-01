
namespace Basket.Api.Features.Carts.GetCart;

public class GetCartQueryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/carts/{userId}", async (Guid userId, ISender sender) =>
        {
            var response = await sender.Send(new GetCartQuery() { UserId = userId });
            return Results.Created($"api/carts/{response.UserId}", response);
        })
        .WithName("GetCart")
        .Produces<Cart>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get user's cart.");
    }
}
