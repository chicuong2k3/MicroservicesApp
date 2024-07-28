// Fix later

using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Commands;
using Ordering.Domain.Enums;

namespace Ordering.Api.Endpoints;

public record UpdateOrderRequest(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    UpdateAddressDto ShippingAddress,
    UpdatePaymentDto Payment,
    List<UpdateOrderItemDto> OrderItems,
    OrderStatus Status
);
public class UpdateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/api/orders/{id}", async (Guid id, [FromBody] UpdateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateOrderCommand>();
            await sender.Send(command);
            return Results.Ok();
        })
        .WithName("UpdateOrder")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update order.");
    }
}
