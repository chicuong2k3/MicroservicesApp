// Fix later

using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Api.Endpoints;

public class UpdateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/orders/{id}", async (Guid id, [FromBody] UpdateOrderCommand request, ISender sender) =>
        {
            await sender.Send(request);
            return Results.Ok();
        })
        .WithName("UpdateOrder")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update order.");
    }
}
