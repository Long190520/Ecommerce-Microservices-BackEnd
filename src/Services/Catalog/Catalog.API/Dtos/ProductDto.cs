namespace Catalog.API.Dtos
{
    public class ProductDto
    {
    }

    public class ProductWithVariantsDto
    {
        public Product Product { get; set; } = default!;
        public IReadOnlyList<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
