namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders.Select(order => new OrderDto(
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
            ));
        }
    }
}
