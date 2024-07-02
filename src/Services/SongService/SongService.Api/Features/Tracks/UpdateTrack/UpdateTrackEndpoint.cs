using Microsoft.AspNetCore.Mvc;

namespace SongService.Api.Features.Tracks.UpdateTrack;

public class UpdateTrackRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ThumbUrl { get; set; } = default!;
    public List<string> Genres { get; set; } = new();
}
public class UpdateTrackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/tracks/{id}", async (Guid id, [FromBody] UpdateTrackRequest request, ISender sender) =>
        {
            var command = request.Adapt<UpdateTrackCommand>();
            command.Id = id;
            await sender.Send(command);
            return Results.Ok("Track updated.");
        })
        .WithName("UpdateTrack")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update a track.");
    }
}
