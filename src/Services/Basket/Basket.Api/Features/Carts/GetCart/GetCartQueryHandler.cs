
namespace Basket.Api.Features.Carts.GetCart;

public class GetCartQuery : IQuery<Cart>
{
    public Guid UserId { get; set; }
}

internal class GetCartQueryHandler(ICartRepository cartRepository)
    : IQueryHandler<GetCartQuery, Cart>
{
    public async Task<Cart> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(query.UserId);

        return cart;
    }
}
