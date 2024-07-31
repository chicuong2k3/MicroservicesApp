using Carter;
using MediatR;
using Ordering.Application.Features.Orders.Queries;

namespace Ordering.Api.Endpoints;
public class GetOrdersEndpointEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", async ([AsParameters] GetOrdersQuery request, ISender sender) =>
        {
            var response = await sender.Send(request);
            return Results.Ok(response);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get several orders.");
    }
}
