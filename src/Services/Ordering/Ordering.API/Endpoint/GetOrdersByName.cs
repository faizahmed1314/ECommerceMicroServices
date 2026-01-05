using Ordering.Application.Orders.Quries.GetOrderByName;

namespace Ordering.API.Endpoint
{
    // accept the order name as a parameter
    // construct a GetOrdersByNameQuery
    // use the mediator to send the query to the appropriate handler
    // return a response containing the list of orders matching the specified name

    public record GetOrdersByNameRequest(string OrderName);
    public record GetOrdersByNameResponse(List<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{ordername}", async (string ordername, ISender sender) =>
            {
                var query = new GetOrderByNameQuery(ordername);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByNameResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrdersByName")
                .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets orders by name")
                .WithDescription("Retrieves a list of orders matching the specified name.");
        }
    }
}
