using Catalog.API.Products.GetProductsByCategory;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryRequest(Guid id);
    public record GetProductsByCategoryResponse(IReadOnlyList<Product> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(id));

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductByCategory")
            .WithDescription("Get Product By Category");
        }
    }
}
