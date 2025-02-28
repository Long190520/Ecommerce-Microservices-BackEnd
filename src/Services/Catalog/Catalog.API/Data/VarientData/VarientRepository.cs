namespace Catalog.API.Data.ProductData
{
    public class VariantRepository
        : Repository<Variant>, IVariantRepository
    {
        private readonly IDocumentSession _session;
        public VariantRepository(IDocumentSession session)
            : base(session)
        {
            _session = session;
        }

        public async Task<IReadOnlyList<Variant>> GetVariantsByProductId(Guid productId, CancellationToken cancellationToken = default)
        {
            var productVariant = await _session.Query<Variant>()
                .Where(p => p.ProductId == productId)
                .ToListAsync();

            if (productVariant == null || !productVariant.Any())
            {
                throw new VariantsNotFoundException();
            }

            return productVariant;
        }
    }

    public interface IVariantRepository : IRepository<Variant>
    {
        Task<IReadOnlyList<Variant>> GetVariantsByProductId(Guid productId, CancellationToken cancellationToken = default);
    }
}
