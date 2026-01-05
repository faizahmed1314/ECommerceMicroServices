using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoint
{
    // accept a updater order request payload
    // map the request to an update order command
    // use the mediator to send the command to the appropriate handler
    // return a response indicating the result of the update operation

    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSuccess);
    public class UpdateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateOrderResponse>();
                return Results.Ok(response);
            })
                .WithName("UpdateOrder")
                .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Updates an existing order")
                .WithDescription("Updates an existing order with the provided details.");
        }
    }
}
