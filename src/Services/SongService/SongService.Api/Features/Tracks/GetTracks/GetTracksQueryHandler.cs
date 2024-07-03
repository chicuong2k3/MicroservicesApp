
namespace SongService.Api.Features.Tracks.GetTracks;

public class GetTracksResult
{
    public IEnumerable<Track> Tracks { get; set; } = new List<Track>();
}
public class GetTracksQuery : IQuery<GetTracksResult>
{
    public string? Genre { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}

internal class GetTracksQueryHandler(IDocumentSession documentSession)
    : IQueryHandler<GetTracksQuery, GetTracksResult>
{
    public async Task<GetTracksResult> Handle(GetTracksQuery query, CancellationToken cancellationToken)
    {
        IQueryable<Track> tracks;
        if (string.IsNullOrEmpty(query.Genre))
        {
            tracks = documentSession.Query<Track>().Where(x => true);
        }
        else
        {
            tracks = documentSession.Query<Track>()
                .Where(x => x.Genres.Contains(query.Genre));
        }

        return new GetTracksResult()
        {
            Tracks = await tracks.ToPagedListAsync(query?.PageNumber ?? 1, query?.PageSize ?? 10, cancellationToken)
        };


    }
}
