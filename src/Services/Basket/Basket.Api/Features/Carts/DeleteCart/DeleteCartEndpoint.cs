
using Basket.Api.Features.Carts.DeleteCart;

namespace Basket.Api.Features.Carts.StoreCartItem;

public class DeleteCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/carts/{userName}", async (string userName, ISender sender) =>
        {
            var command = new DeleteCartCommand() { UserName = userName };
            await sender.Send(command);
            return Results.NoContent();
        })
        .WithName("DeleteCart")
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete cart.");
    }
}
