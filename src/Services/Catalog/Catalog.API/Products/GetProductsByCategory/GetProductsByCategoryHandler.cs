
namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(Guid categoryId) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IReadOnlyList<Product> Products);

    internal class GetProductsHandler
        (IProductRepository repository)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await repository.GetProductByCategory(query.categoryId, cancellationToken);

            return new GetProductsByCategoryResult(products);
        }
    }
}
