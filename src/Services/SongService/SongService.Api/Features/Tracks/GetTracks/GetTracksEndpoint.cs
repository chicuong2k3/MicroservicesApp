namespace SongService.Api.Features.Tracks.GetTracks;

public class GetTracksRequest
{

}
public class GetTracksResponse
{
    public IEnumerable<Track> Tracks { get; set; } = new List<Track>();
}
public class GetTracksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/songs", async (/*GetSongsRequest request, */ISender sender) =>
        {
            //var query = request.Adapt<GetSongsQuery>();
            //var result = await sender.Send(query);
            var result = await sender.Send(new GetTracksQuery());
            var response = result.Adapt<GetTracksResponse>();
            return Results.Ok(response);
        })
        .WithName("GetTracks")
        .Produces<GetTracksResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("List tracks.");
    }
}
