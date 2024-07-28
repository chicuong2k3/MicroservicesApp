using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Api.Endpoints;

public record CreateOrderRequest(
    Guid CustomerId,
    string OrderName,
    CreateAddressDto ShippingAddress,
    CreatePaymentDto Payment,
    List<CreateOrderItemDto> OrderItems
);
public record CreateOrderResponse(Guid Id);
public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (CreateOrderRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateOrderCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateOrderResponse>();
            return Results.Created($"/api/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create new order.");
    }
}
