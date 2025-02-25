namespace Basket.API.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; } = default!;
    }
}
