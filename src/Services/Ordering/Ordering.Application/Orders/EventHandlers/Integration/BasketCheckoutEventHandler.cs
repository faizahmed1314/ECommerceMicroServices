using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enum;

namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            // Todo: Implement the logic to create an order based on the BasketCheckoutEvent data and start order processing workflow
            logger.LogInformation("Integration event handled: {EventId} - {EventName} - {@EventData}", context.Message.Id, context.Message.GetType().Name, context.Message);

            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            // Create full order with incoming event data

            var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, string.Empty, message.ZipCode, string.Empty, message.State, message.Country);
            var paymentDto = new PaymentDto(message.PaymentMethod, message.CardNumber, message.CardName, message.Expiration, message.CVV);

            var orderId = Guid.NewGuid(); // Generate a new order ID

            var orderDto = new OrderDto
            (
                Id: orderId,
                CustomerId: message.CustomerId,
                OrderName: message.UserName + "'s Order",
                ShippingAddress: addressDto,
                BillingAddress: addressDto,
                Payment: paymentDto,
                Status: OrderStatus.Pending,
                OrderItems:
                [
                    // change the guid with actual product ids and unit prices and quantities based on the incoming event data (message)
                    new OrderItemDto(orderId, Guid.Parse("e1a2b3c4-d5e6-7f8a-9b0c-d1e2f3a4b5c6"), 1200, 2), // Example item with total price as unit price and quantity of 1
                    new OrderItemDto(orderId, Guid.Parse("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4e3d2c1b"), 700, 1) // Example item with total price as unit price and quantity of 1
                ]);

            return new CreateOrderCommand(orderDto);
        }
    }
}
