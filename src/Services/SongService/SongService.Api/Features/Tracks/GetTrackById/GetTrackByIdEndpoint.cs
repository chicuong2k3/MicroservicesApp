

namespace SongService.Api.Features.Tracks.GetTrackById;

public class GetTrackByIdResponse
{
    public Track Track { get; set; } = new();
}
public class GetTrackByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tracks/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetTrackByIdQuery() { Id = id });
            var response = result.Adapt<GetTrackByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetTrackById")
        .Produces<GetTrackByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get track by id.");
    }
}
