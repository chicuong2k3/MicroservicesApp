namespace SongService.Api.Features.Tracks.UpdateTrack;

public class UpdateTrackCommand : ICommand 
{ 
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ThumbUrl { get; set; } = default!;
    public List<string> Genres { get; set; } = new();
}
internal class UpdateTrackCommandHandler(IDocumentSession documentSession)
    : ICommandHandler<UpdateTrackCommand>
{
    public async Task<Unit> Handle(UpdateTrackCommand command, CancellationToken cancellationToken)
    {
        var track = await documentSession.LoadAsync<Track>(command.Id);

        if (track == null)
        {
            throw new TrackNotFoundException(command.Id);
        }
        
        track.Name = command.Name;
        track.Description = command.Description;
        track.ThumbUrl = command.ThumbUrl;
        track.Genres = command.Genres;

        documentSession.Update(track);
        await documentSession.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
