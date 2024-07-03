using Common.Exceptions;

namespace SongService.Api.Exceptions
{
    public class TrackNotFoundException : NotFoundException
    {
        public TrackNotFoundException(Guid id) : base("Track", id)
        {
            
        }

    }
}
