using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Quries.GetOrderByName
{
    public class GetOrderByNamehandler(IApplicationDBContext dBContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
    {
        public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
        {
            var orderlist = await dBContext.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.OrderName.Value.Contains(query.Name))
                .OrderBy(o => o.OrderName.Value)
                .ToListAsync(cancellationToken);

            return new GetOrderByNameResult(orderlist.ToOrderDtoList());
        }
    }
}
