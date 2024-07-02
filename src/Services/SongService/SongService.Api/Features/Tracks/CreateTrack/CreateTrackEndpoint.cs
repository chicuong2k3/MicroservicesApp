using Microsoft.AspNetCore.Mvc;

namespace SongService.Api.Features.Tracks.CreateTrack;

public class CreateTrackRequest
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ThumbUrl { get; set; } = default!;
    public List<string> Genres { get; set; } = new();
}
public class CreateTrackResponse
{
    public Guid Id { get; set; }
}
public class CreateTrackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/tracks", async ([FromBody] CreateTrackRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateTrackCommand>();
            var result = await sender.Send(command);
            var response = result.Adapt<CreateTrackResponse>();
            return Results.Created($"/tracks/{response.Id}", response);
        })
        .WithName("CreateTrack")
        .Produces<CreateTrackResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create a new track.");
    }
}
