namespace Catalog.API.Exceptions
{
    public class ProductVariantsNotFoundException : NotFoundException
    {
        public ProductVariantsNotFoundException() : base("Product contains no variant!")
        {
        }
    }
}
