namespace Basket.API.Data
{
    public class BasketRepository
        (IDocumentSession session)
        : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(Guid userId, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userId, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userId) : basket;
        }

        public async Task<ShoppingCart> CreateBasket(Guid userId, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userId, cancellationToken);

            if (basket != null)
            {
                throw new DuplicateBasketException("Basket alredy exist!");
            }

            var newBasket = new ShoppingCart
            {
                UserId = userId,
            };

            session.Store(newBasket);
            await session.SaveChangesAsync();

            return newBasket;
        }

        public async Task<bool> DeleteBasket(Guid userId, CancellationToken cancellationToken = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userId, cancellationToken);

            if (basket != null)
            {
                throw new BasketNotFoundException(userId);
            }

            session.Delete(userId);
            await session.SaveChangesAsync();

            return true;
        }
    }
}
