namespace Basket.API.Basket.CreateBasket
{
    public record CreateBasketCommand(Guid UserId)
        : ICommand<CreateBasketResult>;
    public record CreateBasketResult(ShoppingCart Cart);
    internal class CreateBasketHandler 
        (IBasketRepository repository)
        : ICommandHandler<CreateBasketCommand, CreateBasketResult>
    {
        public async Task<CreateBasketResult> Handle(CreateBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.CreateBasket(command.UserId);
            return new CreateBasketResult(basket);
        }
    }
}
