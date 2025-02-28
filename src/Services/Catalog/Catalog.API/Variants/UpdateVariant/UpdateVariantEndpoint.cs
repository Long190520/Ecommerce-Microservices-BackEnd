namespace Catalog.API.Products.UpdateVariant
{
    public record UpdateVariantRequest(Guid Id, string Color, string Size, decimal Price, int StockQuantity, string ImageFile);
    public record UpdateVariantResponse(bool IsSuccess);

    public class UpdateVariantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateVariantRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateVariantCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateVariantResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateVariant")
            .Produces<UpdateVariantResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("UpdateVariant")
            .WithDescription("Update a product variant");
        }
    }
}
