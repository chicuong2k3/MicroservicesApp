
namespace Ordering.Application.Features.Orders.Queries;


public class GetOrdersByCustomerResult
{
    public List<OrderDto> Orders { get; set; } = new();
}
public class GetOrdersByCustomerQuery : IQuery<GetOrdersByCustomerResult>
{
    public Guid CustomerId { get; set; }
}
public class GetOrdersByCustomerQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(x => x.CustomerId == CustomerId.Generate(query.CustomerId))
            .OrderBy(x => x.OrderName.Value)
            .Include(x => x.OrderItems)
            .ToListAsync(cancellationToken);

        

        return new GetOrdersByCustomerResult() 
        {
            Orders = orders.ToOrderDtos()
        };
    }

    
}
