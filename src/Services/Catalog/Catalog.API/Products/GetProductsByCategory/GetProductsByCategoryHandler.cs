namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(Guid Id) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IReadOnlyList<Product> Products);

    internal class GetProductsHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(p=>p.CategoryId == query.Id)
                .ToListAsync();

            return new GetProductsByCategoryResult(products);
        }
    }
}
