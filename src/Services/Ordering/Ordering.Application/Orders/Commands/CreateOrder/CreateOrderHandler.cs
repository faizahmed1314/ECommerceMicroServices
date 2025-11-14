

namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDBContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            // Implementation to create an order goes here
            // save to database, publish events, etc.
            // return result with new order ID


            var order = CreateNewOrder(command.Order);
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);


            return new CreateOrderResult(order.Id.Value);
        }

        private Order CreateNewOrder(OrderDto order)
        {
            var billingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName,
                order.BillingAddress.EmailAddess, order.BillingAddress.AddressLine,
                order.BillingAddress.Streat, order.BillingAddress.PostalCode,
                order.BillingAddress.City, order.BillingAddress.State, order.BillingAddress.Country);

            var shippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddess,
                order.ShippingAddress.AddressLine, order.ShippingAddress.Streat,
                order.ShippingAddress.PostalCode, order.ShippingAddress.City,
                order.ShippingAddress.State, order.ShippingAddress.Country);

            var newOrder = Order.Create(
                id: OrderId.Of(Guid.NewGuid()),
                customerId: CustomerId.Of(order.CustomerId),
                orderName: OrderName.Of(order.OrderName),
                shippingAddress: shippingAddress,
                billingAddress: billingAddress,
                Payment.Of(order.Payment.PaymentMethod, order.Payment.CardNumber,
                    order.Payment.CardHolderName, order.Payment.Cvv, order.Payment.Expiration)
            );

            foreach (var item in order.OrderItems)
            {
                newOrder.AddOrderItem(
                    productId: ProductId.Of(item.ProductId),
                    price: item.UnitPrice,
                    quantity: item.Quantity
                );
            }
            return newOrder;
        }
    }
}
