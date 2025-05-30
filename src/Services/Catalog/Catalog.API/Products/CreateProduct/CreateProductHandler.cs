namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, string Description, string ImageFile, decimal Price, List<string> Category) : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Create product entity froma command object

            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };

            // Todo
            // save it to the database

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            // return the result

            return new CreateProductResult(product.Id);

        }
    }
}
