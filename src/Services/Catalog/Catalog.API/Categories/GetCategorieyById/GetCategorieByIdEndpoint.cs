using Catalog.API.Categories.GetCategoryById;
using Catalog.API.Products.DeleteProduct;

namespace Catalog.API.CategoryById.GetCategoryByIdByCategory
{
    public record GetCategoryByIdRequest(Guid categoryId);
    public record GetCategoryByIdResponse(IReadOnlyList<Category> CategoryById);

    public class GetCategoryByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/categories/{categoryId}", async (Guid categoryId, ISender sender) =>
            {
                var result = await sender.Send(new GetCategoryByIdQuery(categoryId));

                var response = result.Adapt<GetCategoryByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCategoryById")
            .Produces<GetCategoryByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetCategoryById")
            .WithDescription("Get CategoryById");
        }
    }
}
