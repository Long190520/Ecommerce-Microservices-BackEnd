using Catalog.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Catalog.API.Data.ProductData
{
    public class CacheProductVariantRepository
        (IProductVariantRepository repository, IDistributedCache cache)
        : IProductVariantRepository
    {
        public async Task<ProductVariant> AddAsync(ProductVariant productVariant, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"variant_{productVariant.Id.ToString()}";
            var serializedProductVariant = JsonSerializer.Serialize(productVariant);
            await cache.SetStringAsync(cacheKey, serializedProductVariant, cancellationToken);

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

        public async Task<ProductVariant> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var cachedProductVariant = await cache.GetStringAsync(Id.ToString(), cancellationToken);

            if (!string.IsNullOrEmpty(cachedProductVariant))
                return JsonSerializer.Deserialize<ProductVariant>(cachedProductVariant)!;

            var productVariant = await repository.GetByIdAsync(Id, cancellationToken);

            if (productVariant == null)
            {
                throw new ProductVariantNotFoundException(Id);
            }

            await cache.SetStringAsync(Id.ToString(), JsonSerializer.Serialize(productVariant), cancellationToken);
            return productVariant;
        }

        public async Task<IEnumerable<ProductVariant>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var productVariants = await repository.GetAllAsync(cancellationToken);
            if (productVariants == null || !productVariants.Any())
            {
                throw new ProductVariantsNotFoundException();
            }
            return productVariants;
        }

        public async Task<ProductVariant> UpdateAsync(ProductVariant productVariant, CancellationToken cancellationToken = default)
        {
            var res = await repository.UpdateAsync(productVariant, cancellationToken);

            await cache.RemoveAsync(productVariant.Id.ToString(), cancellationToken);

            var cacheKey = $"variant_{res.Id.ToString()}";
            var serializedProductVariant = JsonSerializer.Serialize(res);
            await cache.SetStringAsync(cacheKey, serializedProductVariant, cancellationToken);

            return res;
        }

        public async Task<IReadOnlyList<ProductVariant>> GetProductVariantsByProductId(Guid productId, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"product_{productId}_variants";
            var cachedProductVariants = await cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedProductVariants))
            {
                return JsonSerializer.Deserialize<IReadOnlyList<ProductVariant>>(cachedProductVariants) ?? new List<ProductVariant>();
            }

            var productVariant = await repository.GetProductVariantsByProductId(productId,cancellationToken);

            if (productVariant == null || !productVariant.Any())
            {
                throw new ProductVariantsNotFoundException();
            }

            return productVariant;
        }

    }
}
