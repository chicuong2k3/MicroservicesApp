namespace SongService.Api.Models
{
    public class Track
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ThumbUrl { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public List<string> Genres { get; set; } = new();
    }
}
