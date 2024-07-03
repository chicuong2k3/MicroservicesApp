

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

public class CreateTrackCommandValidator : AbstractValidator<CreateTrackCommand>
{
    public CreateTrackCommandValidator()
    {

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .Length(5, 100).WithMessage("Name must have between 5 and 100 characters.");

        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.")
            .Length(10, 500).WithMessage("Description must have between 10 and 500 characters..");

        RuleFor(x => x.ThumbUrl).NotEmpty().WithMessage("ThumbUrl is required.")
            .MaximumLength(1024).WithMessage("ThumbUrl must have less than 1024 characters.");
    }
}
internal class CreateTrackCommandHandler(
    IDocumentSession documentSession)
    : ICommandHandler<CreateTrackCommand, CreateTrackResult>
{
    public async Task<CreateTrackResult> Handle(CreateTrackCommand command, CancellationToken cancellationToken)
    {

        var track = new Track()
        {
            Name = command.Name,
            Description = command.Description,
            ThumbUrl = command.ThumbUrl,
            CreatedAt = DateTime.Now,
            Genres = command.Genres
        };

        documentSession.Store(track);
        await documentSession.SaveChangesAsync();

        return new CreateTrackResult() { Id = track.Id };
    }
}
