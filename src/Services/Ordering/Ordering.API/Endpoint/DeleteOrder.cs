using Ordering.Application.Orders.Commands.DeleteOrders;

namespace Ordering.API.Endpoint
{
    // accepts the order id as a parameter
    // Constructs a delete order command
    // uses the mediator to send the command to the appropriate handler
    // returns a response indicating the result of the delete operation

    public record DeleteOrderRequest(Guid OrderId);
    public record DeleteOrderResponse(bool IsSuccess);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteOrderCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteOrder")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Deletes an existing order")
                .WithDescription("Deletes an existing order with the provided id.");
        }
    }
}
