using Microsoft.AspNetCore.Mvc;

namespace SongService.Api.Features.Tracks.GetTracks;

public class GetTracksResult
{
    public IEnumerable<Track> Tracks { get; set; } = new List<Track>();
}
public class GetTracksQuery : IQuery<GetTracksResult>
{
}

internal class GetTracksQueryHandler(IDocumentSession documentSession)
    : IQueryHandler<GetTracksQuery, GetTracksResult>
{
    public async Task<GetTracksResult> Handle([FromQuery] GetTracksQuery query, CancellationToken cancellationToken)
    {
        var tracks = await documentSession.Query<Track>().ToListAsync();

        return new GetTracksResult()
        {
            Tracks = tracks
        };
    }
}
