namespace Catalog.API.Exceptions
{
    public class ProductVariantNotFoundException : NotFoundException
    {
        public ProductVariantNotFoundException(Guid Id) : base("Product Variant", Id)
        {
        }
    }
}
