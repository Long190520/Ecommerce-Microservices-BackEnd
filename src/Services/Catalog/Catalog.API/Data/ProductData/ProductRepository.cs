namespace Catalog.API.Data.ProductData
{
    public class ProductRepository
        : Repository<Product>, IProductRepository
    {
        private readonly IDocumentSession _session;
        public ProductRepository(IDocumentSession session)
            : base(session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<Product>> GetProductByCategory(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var category = await _session.LoadAsync<Category>(categoryId, cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(categoryId);
            }

            var products = await _session.Query<Product>()
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (products == null || !products.Any())
            {
                throw new ProductsNotFoundException();
            }

            return products;
        }
    }

    public interface IProductRepository : IRepository<Product>
    {
        Task<IReadOnlyList<Product>> GetProductByCategory(Guid categoryId, CancellationToken cancellationToken = default);
    }
}
