

namespace Favourite.Api.Data
{
    public class FavouriteItemRepository : IFavouriteItemRepository
    {
        public Task<FavouriteItem> CreateItemAsync(FavouriteItem item)
        {
            throw new NotImplementedException();
        }

        public Task<FavouriteItem> GetItemByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(FavouriteItem item)
        {
            throw new NotImplementedException();
        }
    }
}
