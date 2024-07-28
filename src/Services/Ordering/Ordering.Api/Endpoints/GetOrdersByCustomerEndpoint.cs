using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Queries;

namespace Ordering.Api.Endpoints;

public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
public class GetOrdersByCustomerEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
        {
            var command = new GetOrdersByCustomerQuery() { CustomerId = customerId };
            var result = await sender.Send(command);
            var response = result.Adapt<GetOrdersByCustomerResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrdersByCustomer")
        .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get orders of a customer.");
    }
}
