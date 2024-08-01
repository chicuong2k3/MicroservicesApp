
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    public class CachedCartRepository(ICartRepository cartRepository, IDistributedCache cache) 
        : ICartRepository
    {
        public async Task DeleteCartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            await cartRepository.DeleteCartAsync(userId, cancellationToken);

            await cache.RemoveAsync(userId.ToString(), cancellationToken);
        }

        public async Task<Cart> GetCartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var cachedCartJson = await cache.GetStringAsync(userId.ToString(), cancellationToken);

            if (!string.IsNullOrEmpty(cachedCartJson))
            {
                var cachedCart = JsonSerializer.Deserialize<Cart>(cachedCartJson);
                if (cachedCart != null)
                {
                    return cachedCart;
                }
            }

            var cart = await cartRepository.GetCartAsync(userId, cancellationToken);
            await cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }

        public async Task<Cart> StoreCartItemAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await cartRepository.StoreCartItemAsync(cart, cancellationToken);
            await cache.SetStringAsync(cart.UserId.ToString(), JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }
    }
}
