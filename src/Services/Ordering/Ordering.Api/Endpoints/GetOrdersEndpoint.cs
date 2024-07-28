using Carter;
using Common.Pagination;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Features.Orders.Queries;

namespace Ordering.Api.Endpoints;

public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
public class GetOrdersEndpointEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
        {
            var command = new GetOrdersQuery() { PaginationRequest = request };
            var result = await sender.Send(command);
            var response = result.Adapt<GetOrdersResponse>();
            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get several orders.");
    }
}
