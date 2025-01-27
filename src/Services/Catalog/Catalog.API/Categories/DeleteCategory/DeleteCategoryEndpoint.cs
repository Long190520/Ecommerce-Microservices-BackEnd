namespace Catalog.API.Categories.DeleteCategory
{
    public record DeleteCategoryRequest(string Name);
    public record DeleteCategoryResponse(bool IsSuccess);

    public class DeleteCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/categories", async (DeleteCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<DeleteCategoryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<DeleteCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteCategory")
            .Produces<DeleteCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("DeleteCategory")
            .WithDescription("Delete a category");
        }
    }
}
