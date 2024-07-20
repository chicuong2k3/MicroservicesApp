using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(string userName, CancellationToken cancellationToken = default);
        Task<Cart> StoreCartItemAsync(Cart cart, CancellationToken cancellationToken = default);
        Task DeleteCartAsync(string userName, CancellationToken cancellationToken = default);
    }
}
