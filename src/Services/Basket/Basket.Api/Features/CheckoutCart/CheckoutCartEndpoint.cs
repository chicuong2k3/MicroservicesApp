
namespace Basket.Api.Features.CheckoutCart;

public class CheckoutCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/carts/checkout", async (CheckoutCartCommand request, ISender sender) =>
        {
            await sender.Send(request);
            return Results.Ok();
        })
        .WithName("CheckoutCart")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Checkout cart");
    }
}
