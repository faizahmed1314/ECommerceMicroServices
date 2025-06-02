
using FluentValidation;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category) : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.").Length(2, 15).WithMessage("Name length should be greater than 2 letter");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }

    internal class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommand> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling UpdateProductCommand.handle is {@Command}", command);

            // Get the product by id from the database
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product is null)
            {
                throw new ProductNotFoundException($"Product with id {command.Id} not found.");
            }
            // Update the product
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Category = command.Category;
            product.ImageFile = command.ImageFile;
            // Save the changes to the database
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Product with id {Id} updated successfully", command.Id);
            // Return the result
            return new UpdateProductResult(true);
        }
    }
}
