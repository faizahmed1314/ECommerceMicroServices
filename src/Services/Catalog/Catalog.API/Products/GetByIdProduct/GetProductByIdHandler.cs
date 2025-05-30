namespace Catalog.API.Products.GetByIdProduct
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByIdQuery {@Query}", query);
            // Get the product by id from the database
            var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (result == null)
            {
                throw new ProductNotFoundException();
            }
            // return the result
            return new GetProductByIdResult(result);
        }
    }
}
