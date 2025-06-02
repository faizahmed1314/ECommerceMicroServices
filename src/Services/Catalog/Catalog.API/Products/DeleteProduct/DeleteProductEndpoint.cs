using Carter;
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid Id): 
    public record DeleteProductResponse(bool isDeleted);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(Id));
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete products Id")
                .WithDescription("Delete product product by id from the database");
        }
    }
}
