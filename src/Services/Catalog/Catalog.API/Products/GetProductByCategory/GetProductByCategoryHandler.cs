
namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQuery> logger) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("Handling GetProductByCategoryQuery handle query {@Query}", query);

            var products = await session.Query<Product>()
                .Where(p => p.Category.Contains(query.Category)).ToListAsync(cancellationToken);

            return products.Count == 0
                ? throw new ProductNotFoundException($"No products found in category {query.Category}")
                : new GetProductByCategoryResult(products);
        }
    }
}
