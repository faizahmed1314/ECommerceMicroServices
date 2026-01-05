using BuildingBlocks.Pagination;
using Ordering.Application.Orders.Quries.GetOrders;

namespace Ordering.API.Endpoint
{
    // accept pagination and filtering parameters
    // construct a GetOrderQuery
    // use the mediator to send the query to the appropriate handler
    // return a response containing the list of orders

    //public record GetOrdersRequest(PaginationRequest PaginationRequest);
    public record GetOrdersResponse(PaginationResult<OrderDto> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest paginationRequest, ISender sender) =>
            {
                var query = new GetOrdersQuery(paginationRequest);
                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersResponse>();
                return Results.Ok(response);
            })
                .WithName("GetOrders")
                .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Gets a list of orders")
                .WithDescription("Retrieves a paginated list of orders with optional filtering.");
        }
    }
}
