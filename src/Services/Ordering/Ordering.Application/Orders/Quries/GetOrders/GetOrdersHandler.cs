namespace Ordering.Application.Orders.Quries.GetOrders
{
    public class GetOrdersHandler(IApplicationDBContext dBContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orderlist = await dBContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);
            return new GetOrdersResult(orderlist.ToOrderDtoList());
        }
    }
}
