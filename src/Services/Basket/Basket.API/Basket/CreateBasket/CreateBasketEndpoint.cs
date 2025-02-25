
namespace Basket.API.Basket.CreateBasket
{
    //public record CreateBasketRequest(string UserName);
    public record CreateBasketResponse(ShoppingCart Cart);
    public class CreateBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new CreateBasketCommand(userName));

                var response = result.Adapt<CreateBasketResponse>();

                return Results.Created($"/basket/{userName}", response);
            })
            .WithName("CreateBasket")
            .Produces<CreateBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create a new basket")
            .WithDescription("Create a new basket");
        }
    }
}
