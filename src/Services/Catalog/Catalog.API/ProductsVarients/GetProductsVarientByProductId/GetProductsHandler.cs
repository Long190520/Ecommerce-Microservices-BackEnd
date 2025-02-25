
namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumer = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetCategoriesHandler
        (IProductRepository repository)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await repository.GetProducts(query.PageNumer, query.PageSize, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
