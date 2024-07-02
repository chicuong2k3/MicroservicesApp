namespace SongService.Api.Features.Tracks.DeleteProduct;

public class DeleteTrackEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/tracks/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteTrackCommand() { Id = id });
            return Results.Ok("Track deleted.");
        })
        .WithName("DeleteTrack")
        .Produces(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete a track.");
    }
}
