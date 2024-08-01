
using Basket.Api.Features.Carts.DeleteCart;

namespace Basket.Api.Features.Carts.StoreCartItem;

public class DeleteCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/carts/{userId}", async (Guid userId, ISender sender) =>
        {
            var command = new DeleteCartCommand() { UserId = userId };
            await sender.Send(command);
            return Results.NoContent();
        })
        .WithName("DeleteCart")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete cart.");
    }
}
