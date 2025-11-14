namespace Ordering.Application.Orders.Quries.GetOrderByName
{
    public record GetOrderByNameQuery(string Name) : IQuery<GetOrderByNameResult>;

    public record GetOrderByNameResult(IEnumerable<OrderDto> Orders);

}
