using Basket.Api.Models;

namespace Basket.Api.Data
{
    public class CartRepository(IDocumentSession session) : ICartRepository
    {
        public async Task DeleteCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            session.Delete<Cart>(userName);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task<Cart> GetCartAsync(string userName, CancellationToken cancellationToken = default)
        {
            var cart = await session.LoadAsync<Cart>(userName, cancellationToken);

            if (cart == null)
            {
                throw new CartNotFoundException(userName);
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
