using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface ICartRepository
    {
        Task<Cart> GetCartAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<Cart> StoreCartItemAsync(Cart cart, CancellationToken cancellationToken = default);
        Task DeleteCartAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
