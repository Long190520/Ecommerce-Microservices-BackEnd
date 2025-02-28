namespace Catalog.API.Exceptions
{
    public class VariantsNotFoundException : NotFoundException
    {
        public VariantsNotFoundException() : base("Product contains no variant!")
        {
        }
    }
}

