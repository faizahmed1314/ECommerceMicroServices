
namespace Ordering.Application.Orders.Commands.DeleteOrders
{
    public class DeleteOrderHandler(IApplicationDBContext dBContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.OrderId);
            var order = await dBContext.Orders.FindAsync([orderId], cancellationToken: cancellationToken);

            if (order == null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            dBContext.Orders.Remove(order);
            await dBContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}
