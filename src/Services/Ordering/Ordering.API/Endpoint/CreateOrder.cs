
using Ordering.Application.Orders.Commands.CreateOrder;

namespace Ordering.API.Endpoint
{
    // Accepts a request to create a new order
    // Maps the request to create order command
    // uses the mediator to send the command to the appropriate handler
    // returns a response with the created order id

    public record CreateOrderRequest(OrderDto Order);
    public record CreateOrderResponse(Guid Id);
    public class CreateOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/orders", async (CreateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateOrderResponse>();
                return Results.Created($"/orders/{response.Id}", response);
            })
                .WithName("CreateOrder")
                .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Creates a new order")
                .WithDescription("Creates a new order with the provided details.");
        }
    }
}
