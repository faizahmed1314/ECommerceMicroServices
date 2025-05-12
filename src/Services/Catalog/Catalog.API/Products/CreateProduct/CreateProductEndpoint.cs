using Carter;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(string Name, string Description, string ImageFile, decimal Price, List<string> Category);

    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                // Map the request to a command
                var commnad = request.Adapt<CreateProductCommand>();
                // Send the command to the mediator
                var result = await sender.Send(commnad);
                // Map the result to a response
                var response = result.Adapt<CreateProductResponse>();
                // Return the response
                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new product")
            .WithDescription("Create a new product in the catalog");
        }
    }
}
