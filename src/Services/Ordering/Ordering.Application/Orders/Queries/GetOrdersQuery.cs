namespace Ordering.Application.Orders.Queries
{
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
    public record GetOrdersResult(PaginatedResult<OrderDto> Orders);

    public class GetOrdersQueryHandler(IApplicationDbContext dbContext)
        : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);


            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .OrderBy(o => o.OrderName.Value)
                .AsNoTracking()
                .Skip(pageIndex)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var ordersDto = orders.Select(order => order.ToDto());

            return new GetOrdersResult(
                new PaginatedResult<OrderDto> (
                    pageIndex, 
                    pageSize, 
                    totalCount, 
                    ordersDto
                    )
                );
        }
    }
}
