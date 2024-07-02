namespace SongService.Api.Features.Tracks.GetTracks;

public class GetTracksRequest
{
    public string? Genre { get; set; } = default!;
}
public class GetTracksResponse
{
    public IEnumerable<Track> Tracks { get; set; } = new List<Track>();
}
public class GetTracksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tracks", async ([AsParameters] GetTracksRequest request, ISender sender) =>
        {
            var query = new GetTracksQuery();
            if (!string.IsNullOrEmpty(request.Genre))
            {
                query = request.Adapt<GetTracksQuery>();
            }

            var result = await sender.Send(query);
            var response = result.Adapt<GetTracksResponse>();
            return Results.Ok(response);
        })
        .WithName("GetTracks")
        .Produces<GetTracksResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get several tracks.");
    }
}
