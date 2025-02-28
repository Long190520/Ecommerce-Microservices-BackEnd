using Catalog.API.Categories.GetCategories;

namespace Catalog.API.Categories.GetCategoriesByCategory
{
    public record GetCategoriesRequest(int? PageNumber = 1, int? PageSize = 10);
    public record GetCategoriesResponse(IReadOnlyList<Category> Categories);

    public class GetCategoriesEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/Categories", async ([AsParameters] GetCategoriesRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetCategoriesQuery>();

                var result = await sender.Send(query);

                var response = result.Adapt<GetCategoriesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetCategories")
            .Produces<GetCategoriesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetCategories")
            .WithDescription("Get Categories");
        }
    }
}
