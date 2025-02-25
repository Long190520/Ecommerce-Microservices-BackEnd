namespace Catalog.API.Data.ProductData
{
    public class ProductVariantRepository
        : Repository<ProductVariant>, IProductVariantRepository
    {
        private readonly IDocumentSession _session;
        public ProductVariantRepository(IDocumentSession session)
            : base(session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<ProductVariant>> GetProductVariantsByProductId(Guid productId, CancellationToken cancellationToken = default)
        {
            var productVariant = await _session.Query<ProductVariant>()
                .Where(p => p.ProductId == productId)
                .ToListAsync();

            if (productVariant == null || !productVariant.Any())
            {
                throw new ProductVariantsNotFoundException();
            }

            return productVariant;
        }
    }

    public interface IProductVariantRepository : IRepository<ProductVariant>
    {
        Task<IReadOnlyList<ProductVariant>> GetProductVariantsByProductId(Guid productId, CancellationToken cancellationToken = default);
    }
}
