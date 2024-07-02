namespace SongService.Api.Exceptions
{
    public class TrackNotFoundException : Exception
    {
        public TrackNotFoundException(Guid id) : base($"Track with id={id} not found.")
        {
            
        }

        public TrackNotFoundException(string message) : base(message)
        {
            
        }
    }
}
