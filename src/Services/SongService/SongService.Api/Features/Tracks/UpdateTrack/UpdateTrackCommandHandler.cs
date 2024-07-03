
using SongService.Api.Features.Tracks.CreateTrack;

namespace SongService.Api.Features.Tracks.UpdateTrack;

public class UpdateTrackCommand : ICommand 
{ 
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ThumbUrl { get; set; } = default!;
    public List<string> Genres { get; set; } = new();
}
public class UpdateTrackCommandValidator : AbstractValidator<UpdateTrackCommand>
{
    public UpdateTrackCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");


        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters..");

        RuleFor(x => x.ThumbUrl).NotEmpty().WithMessage("ThumbUrl is required.")
            .MaximumLength(1024).WithMessage("ThumbUrl must have less than 1024 characters.");
    }
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
