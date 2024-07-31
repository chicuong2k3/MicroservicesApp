using Carter;
using MediatR;
using Ordering.Application.Features.Orders.Commands;

namespace Ordering.Api.Endpoints;

public class CreateOrderEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (CreateOrderCommand request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Created($"/api/orders/{response.Id}", response);
        })
        .WithName("CreateOrder")
        .Produces<CreateOrderResult>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create new order.");
    }
}
