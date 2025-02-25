namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(Guid userId, CancellationToken cancellationToken = default!);
        Task<ShoppingCart> CreateBasket(Guid userId, CancellationToken cancellationToken = default!);
        Task<bool> DeleteBasket(Guid userId, CancellationToken cancellationToken = default!);
    }
}
