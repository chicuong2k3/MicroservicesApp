
using Microsoft.AspNetCore.Mvc;

namespace SongService.Api.Features.Tracks.GetTrackById;

public class GetTrackByIdResult
{
    public Track Track { get; set; } = new();
}
public class GetTrackByIdQuery : IQuery<GetTrackByIdResult>
{
    public Guid Id { get; set; }
}

internal class GetTrackByIdQueryHandler(IDocumentSession documentSession)
    : IQueryHandler<GetTrackByIdQuery, GetTrackByIdResult>
{
    public async Task<GetTrackByIdResult> Handle([FromQuery] GetTrackByIdQuery query, CancellationToken cancellationToken)
    {
        var track = await documentSession.LoadAsync<Track>(query.Id, cancellationToken);

        if (track == null)
        {
            throw new TrackNotFoundException(query.Id);
        }

        return new GetTrackByIdResult()
        {
            Track = track
        };
    }
}
