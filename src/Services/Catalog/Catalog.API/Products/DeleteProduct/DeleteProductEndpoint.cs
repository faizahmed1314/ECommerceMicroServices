using Carter;

namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id);
    public record DeleteProductResponse(bool isDeleted);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            // Fix: Accept Id as a route parameter and fix response/produces types
            app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete product by Id")
            .WithDescription("Delete product by id from the database");
        }
    }
}
