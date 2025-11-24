namespace Ordering.Application.Orders.Quries.GetOrders
{
    public record GetOrdersQuery : IQuery<GetOrdersResult>;

    public record GetOrdersResult(IEnumerable<OrderDto> Orders);
}
