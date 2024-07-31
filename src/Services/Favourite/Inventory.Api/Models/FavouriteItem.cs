namespace Favourite.Api.Models
{
    public class FavouriteItem
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int VariantId { get; set; }
        public DateTime LikedDate { get; set; }
    }
}
