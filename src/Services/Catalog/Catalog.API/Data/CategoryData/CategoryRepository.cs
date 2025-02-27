namespace Catalog.API.Data.CategoryData
{
    public class CategoryRepository
        : Repository<Category>, ICategoryRepository
    {
        private readonly IDocumentSession _session;
        public CategoryRepository(IDocumentSession session)
            : base(session)
        {
            _session = session;
        }

    }

    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
