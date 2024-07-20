
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Features.Carts.StoreCartItem;

public class StoreCartItemRequest
{
    public Cart Cart { get; set; } = default!;
}

public class StoreCartItemResponse
{
    public string UserName { get; set; } = default!;
}
public class StoreCartItemEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/carts", async ([FromBody] StoreCartItemRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreCartItemCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<StoreCartItemResponse>();
            return Results.Ok(response);
        })
        .WithName("StoreCartItem")
        .Produces<Cart>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store an item into a cart.");
    }
}
