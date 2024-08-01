

namespace Basket.Api.Exceptions
{
    public class CartNotFoundException : NotFoundException
    {
        public CartNotFoundException(Guid userId) : base(nameof(Cart), userId)
        {

        }
    }
}
