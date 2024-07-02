namespace SongService.Api.Features.Tracks.CreateTrack;

public class CreateTrackResult
{
    public Guid Id { get; set; }
}
public class CreateTrackCommand : ICommand<CreateTrackResult>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ThumbUrl { get; set; } = default!;
    public List<string> Genres { get; set; } = new();
}
internal class CreateTrackCommandHandler(IDocumentSession documentSession)
    : ICommandHandler<CreateTrackCommand, CreateTrackResult>
{
    public async Task<CreateTrackResult> Handle(CreateTrackCommand command, CancellationToken cancellationToken)
    {
        var track = new Track()
        {
            Name = command.Name,
            Description = command.Description,
            ThumbUrl = command.ThumbUrl,
            CreatedAt = DateTime.Now
        };

        documentSession.Store(track);
        await documentSession.SaveChangesAsync();

        return new CreateTrackResult() { Id = track.Id };
    }
}
