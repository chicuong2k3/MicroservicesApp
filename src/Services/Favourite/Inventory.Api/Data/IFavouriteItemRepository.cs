using Favourite.Api.Models;

namespace Favourite.Api.Data
{
    public interface IFavouriteItemRepository
    {
        Task<FavouriteItem> GetItemByIdAsync(Guid id);
        Task<FavouriteItem> CreateItemAsync(FavouriteItem item);
        Task UpdateItemAsync(FavouriteItem item);
        Task DeleteItemAsync(Guid id);
    }
}
