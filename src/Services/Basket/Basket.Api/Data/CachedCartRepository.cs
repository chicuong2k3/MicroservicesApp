
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
    public class CachedCartRepository(ICartRepository cartRepository, IDistributedCache cache) 
        : ICartRepository
    {
        public async Task DeleteCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            await cartRepository.DeleteCartAsync(userName, cancellationToken);

            await cache.RemoveAsync(userName, cancellationToken);
        }

        public async Task<Cart> GetCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cachedCartJson = await cache.GetStringAsync(userName, cancellationToken);

            if (!string.IsNullOrEmpty(cachedCartJson))
            {
                var cachedCart = JsonSerializer.Deserialize<Cart>(cachedCartJson);
                if (cachedCart != null)
                {
                    return cachedCart;
                }
            }

            var cart = await cartRepository.GetCartAsync(userName, cancellationToken);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }

        public async Task<Cart> StoreCartItemAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            await cartRepository.StoreCartItemAsync(cart, cancellationToken);
            await cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }
    }
}
