

namespace Basket.Api.Exceptions
{
    public class CartNotFoundException : NotFoundException
    {
        public CartNotFoundException(string userName) : base(nameof(Cart), userName)
        {

        }
    }
}
