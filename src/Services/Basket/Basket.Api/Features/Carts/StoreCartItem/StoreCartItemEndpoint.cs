
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Features.Carts.StoreCartItem;

public class StoreCartItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/carts", async ([FromBody] StoreCartItemCommand request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Ok(response);
        })
        .WithName("StoreCartItem")
        .Produces<StoreCartItemResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store an item into a cart.");
    }
}
