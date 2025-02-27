
using Catalog.API.Models;

namespace Catalog.API.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDocumentSession _session;
        public Repository(IDocumentSession session)
        {
            _session = session;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _session.LoadAsync<T>(id, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _session.Query<T>().ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagingAsync(int PageNumber = 1, int PageSize = 10, CancellationToken cancellationToken = default)
        {
            return await _session.Query<T>()
                .ToPagedListAsync(PageNumber, PageSize, cancellationToken);
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _session.Store(entity);
            await _session.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _session.Store(entity);
            await _session.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _session.LoadAsync<T>(id, cancellationToken);
            if (entity != null)
            {
                _session.Delete(entity);
                await _session.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
    }
}
