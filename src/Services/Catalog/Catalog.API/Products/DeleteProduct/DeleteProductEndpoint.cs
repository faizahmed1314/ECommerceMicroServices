using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.DeleteProduct
{
    public class DeleteProductEndpoint
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteProducts")
                .Produces<GetProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Delete products Id")
                .WithDescription("Get all products from the database");
        }
    }
}
