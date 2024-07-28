
using Common.Pagination;

namespace Ordering.Application.Features.Orders.Queries;


public class GetOrdersResult
{
    public PaginatedResult<OrderDto> Orders { get; set; } = default!;
}
public class GetOrdersQuery : IQuery<GetOrdersResult>
{
    public PaginationRequest PaginationRequest { get; set; } = default!;
}
public class GetOrdersQueryHandler(IAppDbContext dbContext)
    : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
    {
        var page = query.PaginationRequest.PageNumber;
        var pageSize = query.PaginationRequest.PageSize;

        var total = await dbContext.Orders.LongCountAsync(cancellationToken);

        var orders = await dbContext.Orders
            .AsNoTracking()
            .OrderBy(x => x.OrderName.Value)
            .Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Include(x => x.OrderItems)
            .ToListAsync(cancellationToken);

        return new GetOrdersResult() 
        {
            Orders = new PaginatedResult<OrderDto>
            (
                page, 
                pageSize, 
                total, 
                orders.ToOrderDtos()
            )
        };
    }

    
}
