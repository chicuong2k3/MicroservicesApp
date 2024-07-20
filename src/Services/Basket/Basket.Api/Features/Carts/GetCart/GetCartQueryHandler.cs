
namespace Basket.Api.Features.Carts.GetCart;

public class GetCartResult
{
    public Cart Cart { get; set; } = new();
}

public class GetCartQuery : IQuery<GetCartResult>
{
    public string UserName { get; set; } = default!;
}

internal class GetCartQueryHandler(ICartRepository cartRepository)
    : IQueryHandler<GetCartQuery, GetCartResult>
{
    public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartAsync(query.UserName);

        return new GetCartResult()
        {
            Cart = cart
        };
    }
}
