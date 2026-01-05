using Ordering.Application.Orders.Quries.GetOrdersByCustomer;

namespace Ordering.API.Endpoint
{
    // accept the customer id as a parameter
    // construct a GetOrdersByCustomerQuery
    // use the mediator to send the query to the appropriate handler
    // return a response containing the list of orders for the specified customer
    public record GetOrdersByCustomerRequest(string CustomerId);
    public record GetOrdersByCustomerResponse(List<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var query = new GetOrdersByCustomerQuery(customerId);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByCustomerResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrdersByCustomer")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets orders by customer")
                .WithDescription("Retrieves a list of orders for the specified customer.");
        }
    }
}
