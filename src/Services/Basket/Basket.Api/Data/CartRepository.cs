using Basket.Api.Models;

namespace Basket.Api.Data
{
    public class CartRepository(IDocumentSession session) : ICartRepository
    {
        public async Task DeleteCartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            session.Delete<Cart>(userId);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task<Cart> GetCartAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var cart = await session.LoadAsync<Cart>(userId, cancellationToken);

            if (cart == null)
            {
                throw new CartNotFoundException(userId);
            }

            return cart;
        }

        public async Task<Cart> StoreCartItemAsync(Cart cart, CancellationToken cancellationToken = default)
        {
            session.Store(cart);
            await session.SaveChangesAsync(cancellationToken);
            return cart;
        }
    }
}
