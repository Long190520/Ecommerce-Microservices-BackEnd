namespace Catalog.API.Exceptions
{
    public class ProductsNotFoundException : NotFoundException
    {
        public ProductsNotFoundException() : base("No products available")
        {
        }
    }
}
