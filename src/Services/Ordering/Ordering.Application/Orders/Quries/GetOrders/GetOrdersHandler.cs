using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Quries.GetOrders
{
    public class GetOrdersHandler(IApplicationDBContext dBContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {

            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalRecords = await dBContext.Orders.LongCountAsync(cancellationToken);


            var orderlist = await dBContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .OrderBy(o => o.OrderName.Value)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginationResult<OrderDto>(
                    pageIndex, pageSize, totalRecords, orderlist.ToOrderDtoList()));
        }
    }
}
