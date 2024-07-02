namespace SongService.Api.Features.Tracks.DeleteProduct;
public class DeleteTrackCommand : ICommand
{
    public Guid Id { get; set; }
}
internal class DeleteTrackCommandHandler(IDocumentSession documentSession)
    : ICommandHandler<DeleteTrackCommand>
{
    public async Task<Unit> Handle(DeleteTrackCommand command, CancellationToken cancellationToken)
    {
        
        try
        {
            documentSession.Delete<Track>(command.Id);
            await documentSession.SaveChangesAsync();
        } catch (Exception ex)
        {

        }

        return new Unit();
    }
}
