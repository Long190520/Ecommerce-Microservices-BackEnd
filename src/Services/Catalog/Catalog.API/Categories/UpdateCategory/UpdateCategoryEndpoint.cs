namespace Catalog.API.Categories.UpdateCategory
{
    public record UpdateCategoryRequest(string Name);
    public record UpdateCategoryResponse(bool IsSuccess);

    public class DeleteCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/categories", async (UpdateCategoryRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateCategoryCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateCategoryResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateCategory")
            .Produces<UpdateCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("UpdateCategory")
            .WithDescription("Update a category");
        }
    }
}
