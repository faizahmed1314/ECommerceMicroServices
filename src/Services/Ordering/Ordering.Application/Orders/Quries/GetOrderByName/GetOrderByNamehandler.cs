using Microsoft.EntityFrameworkCore;

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

            var result = ProjectToOrderList(orderlist);
            return new GetOrderByNameResult(result);
        }

        private IEnumerable<OrderDto> ProjectToOrderList(List<Order> orderlist)
        {
            List<OrderDto> orders = new();
            foreach (var order in orderlist)
            {
                var orderDto = new OrderDto(
                    order.Id.Value,
                    order.CustomerId.Value,
                    order.OrderName.Value,
                    new AddressDto(
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.Street,
                        order.ShippingAddress.PostalCode,
                        order.ShippingAddress.City,
                        order.ShippingAddress.State,
                        order.ShippingAddress.Country
                    ),
                    new AddressDto(
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.Street,
                        order.BillingAddress.PostalCode,
                        order.BillingAddress.City,
                        order.BillingAddress.State,
                        order.BillingAddress.Country
                    ),
                    new PaymentDto(
                        order.Payment.PaymentMethod,
                        order.Payment.CardNumber,
                        order.Payment.CardHolderName,
                        order.Payment.CVV,
                        order.Payment.Expiration
                    ),
                    order.Status,
                    order.OrderItems.Select(oi => new OrderItemDto(
                        oi.OrderId.Value,
                        oi.ProductId.Value,
                        oi.Price,
                        oi.Quantity
                    )).ToList()
                );
                orders.Add(orderDto);
            }
            return orders;
        }
    }
}
