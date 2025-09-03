namespace Catalog.API.Products.GetByIdProduct
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);
    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            // Get the product by id from the database
            var result = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (result == null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            // return the result
            return new GetProductByIdResult(result);
        }
    }
}
