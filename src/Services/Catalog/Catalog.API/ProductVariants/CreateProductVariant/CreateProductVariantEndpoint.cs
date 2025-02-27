namespace Catalog.API.ProductVariants.CreateProductVariant
{
    public record CreateProductVariantRequest(string Color, string Size, decimal Price, int StockQuantity, string ImageFile);
    public record CreateProductVariantResponse(Guid Id);

    public class CreateProductVariantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/productVariants", async (CreateProductVariantRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductVariantCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductVariantResponse>();

                return Results.Created($"/productVariants/{response.Id}", response);
            })
            .WithName("CreateProductVariant")
            .Produces<CreateProductVariantResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("CreateProductVariant")
            .WithDescription("Create a product variant");

        }
    }
}
