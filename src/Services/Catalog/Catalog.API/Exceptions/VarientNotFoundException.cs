namespace Catalog.API.Exceptions
{
    public class VariantNotFoundException : NotFoundException
    {
        public VariantNotFoundException(Guid Id) : base("Product Variant", Id)
        {
        }
    }
}
