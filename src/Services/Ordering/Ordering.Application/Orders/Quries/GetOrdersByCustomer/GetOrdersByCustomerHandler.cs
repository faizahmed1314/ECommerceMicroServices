

namespace Ordering.Application.Orders.Quries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDBContext dBContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orderList = await dBContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(o => o.OrderDate)
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orderList.ToOrderDtoList());
        }
    }
}
