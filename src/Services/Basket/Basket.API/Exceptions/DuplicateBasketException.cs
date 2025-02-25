namespace Basket.API.Exceptions
{
    public class DuplicateBasketException : Exception
    {
        public DuplicateBasketException(string message) : base(message)
        {
        }
    }
}
