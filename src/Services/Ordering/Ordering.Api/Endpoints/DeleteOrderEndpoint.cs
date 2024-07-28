using Carter;
using MediatR;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Api.Endpoints;

public class DeleteOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/orders/{id}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteOrderCommand() { OrderId = id };
            await sender.Send(command);
            return Results.Ok();
        })
        .WithName("DeleteOrder")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete order.");
    }
}
