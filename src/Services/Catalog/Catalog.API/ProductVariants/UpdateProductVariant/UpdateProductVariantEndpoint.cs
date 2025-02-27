
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductVariantRequest(Guid Id, string Color, string Size, decimal Price, int StockQuantity, string ImageFile);
    public record UpdateProductVariantResponse(bool IsSuccess);

    public class UpdateProductVariantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products", async(UpdateProductVariantRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductVariantCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateProductVariantResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateProductVariant")
            .Produces<UpdateProductVariantResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("UpdateProductVariant")
            .WithDescription("Update a product variant");
        }
    }
}
