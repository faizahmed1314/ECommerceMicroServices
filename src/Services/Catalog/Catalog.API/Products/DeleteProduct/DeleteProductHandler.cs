
using FluentValidation;

namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsDeleted);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        }
    }

    // Fix: Use ILogger<DeleteProductCommandHandler> instead of ILogger<DeleteProductCommand>
    public class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling DeleteProductCommand.handle is {@Command}", command);
            // Get the product by id from the database
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException($"Product with id {command.Id} not found.");
            }
            // Delete the product
            session.Delete(product);
            // Save the changes to the database
            await session.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Product with id {Id} deleted successfully", command.Id);

            // Return the result
            return new DeleteProductResult(true);
        }
    }
}
