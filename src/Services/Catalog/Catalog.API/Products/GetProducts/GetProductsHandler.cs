namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQuery(int? PageNumer = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetCategoriesHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .ToPagedListAsync(query.PageNumer ?? 1, query.PageSize ?? 1, cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
