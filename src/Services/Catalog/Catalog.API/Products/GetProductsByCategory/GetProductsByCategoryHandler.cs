using Catalog.API.Exceptions;

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
            var category = await session.LoadAsync<Category>(query.Id, cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(query.Id);
            }

            var products = await session.Query<Product>()
                .Where(p=>p.CategoryId == query.Id)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                throw new ProductsNotFoundException();
            }

            return new GetProductsByCategoryResult(products);
        }
    }
}
