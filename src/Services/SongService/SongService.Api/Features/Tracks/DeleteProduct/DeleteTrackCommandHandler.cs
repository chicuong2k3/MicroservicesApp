

namespace SongService.Api.Features.Tracks.DeleteProduct;
public class DeleteTrackCommand : ICommand
{
    public Guid Id { get; set; }
}

public class DeleteTrackCommandValidator : AbstractValidator<DeleteTrackCommand>
{
    public DeleteTrackCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required.");
    }
}
internal class DeleteTrackCommandHandler(IDocumentSession documentSession)
    : ICommandHandler<DeleteTrackCommand>
{
    public async Task<Unit> Handle(DeleteTrackCommand command, CancellationToken cancellationToken)
    {

        documentSession.Delete<Track>(command.Id);
        await documentSession.SaveChangesAsync();

        return new Unit();
    }
}
