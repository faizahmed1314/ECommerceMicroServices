


namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler(IApplicationDBContext dBContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);

            var existingOrder = await dBContext.Orders.FindAsync([orderId], cancellationToken);

            if (existingOrder == null)
            {
                throw new OrderNotFoundException(command.Order.Id);
            }

            UpdateOrderWithNewValue(existingOrder, command.Order);

            dBContext.Orders.Update(existingOrder);

            await dBContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        private void UpdateOrderWithNewValue(Order existingOrder, OrderDto order)
        {
            var updatedBillingAddress = Address.Of(order.BillingAddress.FirstName, order.BillingAddress.LastName,
                order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine,
                order.BillingAddress.Streat, order.BillingAddress.PostalCode,
                order.BillingAddress.City, order.BillingAddress.State, order.BillingAddress.Country);
            var updatedShippingAddress = Address.Of(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress,
                order.ShippingAddress.AddressLine, order.ShippingAddress.Streat,
                order.ShippingAddress.City, order.ShippingAddress.State,
                order.ShippingAddress.PostalCode, order.ShippingAddress.Country);
            var updatedPayment = Payment.Of(order.Payment.PaymentMethod, order.Payment.CardNumber,
                    order.Payment.CardHolderName, order.Payment.Cvv, order.Payment.Expiration);

            existingOrder.Update(
                orderName: OrderName.Of(order.OrderName),
                shippingAddress: updatedShippingAddress,
                BillingAddress: updatedBillingAddress,
                payment: updatedPayment,
                status: order.Status
            );
        }
    }
}
