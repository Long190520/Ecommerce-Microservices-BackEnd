namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryRequest(Guid categoryId);
    public record GetProductsByCategoryResponse(IReadOnlyList<Product> Products);

    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{categoryId}", async (Guid categoryId, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(categoryId));

                var response = result.Adapt<GetProductsByCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductByCategory")
            .Produces<GetProductsByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("GetProductByCategory")
            .WithDescription("Get Product By Category");
        }
    }
}
