using Catalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Catalog.API.Data.ProductData
{
    public class CacheVariantRepository
        (IVariantRepository repository, IDistributedCache cache)
        : IVariantRepository
    {
        public async Task<Variant> AddAsync(Variant productVariant, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"variant_{productVariant.Id.ToString()}";
            var serializedVariant = JsonSerializer.Serialize(productVariant);
            await cache.SetStringAsync(cacheKey, serializedVariant, cancellationToken);

            return productVariant;
        }

        public async Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var productVariant = await repository.GetByIdAsync(Id, cancellationToken);

            var res = await repository.DeleteAsync(Id, cancellationToken);

            if(res == false)
            {
                return res;
            }

            await cache.RemoveAsync($"variant_{Id.ToString()}", cancellationToken);

            return res;
        }

        public async Task<Variant> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var cachedVariant = await cache.GetStringAsync(Id.ToString(), cancellationToken);

            if (!string.IsNullOrEmpty(cachedVariant))
                return JsonSerializer.Deserialize<Variant>(cachedVariant)!;

            var productVariant = await repository.GetByIdAsync(Id, cancellationToken);

            if (productVariant == null)
            {
                throw new VariantNotFoundException(Id);
            }

            await cache.SetStringAsync(Id.ToString(), JsonSerializer.Serialize(productVariant), cancellationToken);
            return productVariant;
        }

        public async Task<IEnumerable<Variant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var productVariants = await repository.GetAllAsync(cancellationToken);
            if (productVariants == null || !productVariants.Any())
            {
                throw new VariantsNotFoundException();
            }
            return productVariants;
        }

        public async Task<Variant> UpdateAsync(Variant productVariant, CancellationToken cancellationToken = default)
        {
            var res = await repository.UpdateAsync(productVariant, cancellationToken);

            await cache.RemoveAsync(productVariant.Id.ToString(), cancellationToken);

            var cacheKey = $"variant_{res.Id.ToString()}";
            var serializedVariant = JsonSerializer.Serialize(res);
            await cache.SetStringAsync(cacheKey, serializedVariant, cancellationToken);

            return res;
        }

        public async Task<IReadOnlyList<Variant>> GetVariantsByProductId(Guid productId, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"product_{productId}_variants";
            var cachedVariants = await cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedVariants))
            {
                return JsonSerializer.Deserialize<IReadOnlyList<Variant>>(cachedVariants) ?? new List<Variant>();
            }

            var productVariant = await repository.GetVariantsByProductId(productId,cancellationToken);

            if (productVariant == null || !productVariant.Any())
            {
                throw new VariantsNotFoundException();
            }

            return productVariant;
        }

        public Task<IPagedList<Variant>> GetPagingAsync(int? PageNumber = 1, int? PageSize = 10, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
