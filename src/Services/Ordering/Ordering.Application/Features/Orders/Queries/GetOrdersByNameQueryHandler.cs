
namespace Ordering.Application.Features.Orders.Queries;


public class GetOrdersByNameResult
{
    public List<OrderDto> Orders { get; set; } = new();
}
public class GetOrdersByNameQuery : IQuery<GetOrdersByNameResult>
{
    public string Name { get; set; } = default!;
}
public class GetOrdersByNameQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(x => x.OrderName.Value.Contains(query.Name))
            .OrderBy(x => x.OrderName.Value)
            .Include(x => x.OrderItems)
            .ToListAsync(cancellationToken);

        return new GetOrdersByNameResult() 
        {
            Orders = orders.ToOrderDtos()
        };
    }

    
}
