using Catalog.API.Models;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Xml.Linq;

namespace Catalog.API.Data.ProductData
{
    public class CacheProductRepository
        (IProductRepository repository, IDistributedCache cache)
        : IProductRepository
    {
        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"product_{product.Id.ToString()}";
            var serializedProduct = JsonSerializer.Serialize(product);
            await cache.SetStringAsync(cacheKey, serializedProduct, cancellationToken);

            await cache.RemoveAsync($"products_category_{product.CategoryId}", cancellationToken);

            return product;
        }

        public async Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var product = await repository.GetByIdAsync(Id, cancellationToken);

            var res = await repository.DeleteAsync(Id, cancellationToken);

            if (res == false)
            {
                return res;
            }

            await cache.RemoveAsync($"product_{Id.ToString()}", cancellationToken);
            await cache.RemoveAsync($"products_category_{product.CategoryId}", cancellationToken);

            return res;
        }

        public async Task<IReadOnlyList<Product>> GetProductByCategory(Guid categoryId, CancellationToken cancellationToken = default)
        {
            var cacheKey = $"products_category_{categoryId}";
            var cachedProducts = await cache.GetStringAsync(cacheKey, cancellationToken);

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                return JsonSerializer.Deserialize<IReadOnlyList<Product>>(cachedProducts) ?? new List<Product>();
            }

            var products = await repository.GetProductByCategory(categoryId, cancellationToken);

            if (products == null || !products.Any())
            {
                throw new ProductsNotFoundException();
            }

            var serializedProducts = JsonSerializer.Serialize(products);
            await cache.SetStringAsync(cacheKey, serializedProducts, cancellationToken);

            return products ?? new List<Product>();
        }

        public async Task<Product> GetByIdAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            var cachedProduct = await cache.GetStringAsync($"product_{Id.ToString()}", cancellationToken);

            if (!string.IsNullOrEmpty(cachedProduct))
                return JsonSerializer.Deserialize<Product>(cachedProduct)!;

            var product = await repository.GetByIdAsync(Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(Id);
            }

            await cache.SetStringAsync(Id.ToString(), JsonSerializer.Serialize(product), cancellationToken);
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await repository.GetAllAsync(cancellationToken);
            if (products == null || !products.Any())
            {
                throw new ProductsNotFoundException();
            }
            return products;
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var res = await repository.UpdateAsync(product, cancellationToken);

            await cache.RemoveAsync(product.Id.ToString(), cancellationToken);
            await cache.RemoveAsync($"products_category_{product.CategoryId}", cancellationToken);

            var cacheKey = $"product_{res.Id.ToString()}";
            var serializedProductVariant = JsonSerializer.Serialize(res);
            await cache.SetStringAsync(cacheKey, serializedProductVariant, cancellationToken);

            return res;
        }

        public async Task<IPagedList<Product>> GetPagingAsync(int? PageNumber = 1, int? PageSize = 10, CancellationToken cancellationToken = default)
        {
            var products = await repository.GetPagingAsync(PageNumber, PageSize, cancellationToken);
            if (products == null || !products.Any())
            {
                throw new ProductsNotFoundException();
            }
            return products;
        }
    }
}
