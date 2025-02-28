namespace Catalog.API.Variants.CreateVariant
{
    public record CreateVariantRequest(string Color, string Size, decimal Price, int StockQuantity, string ImageFile);
    public record CreateVariantResponse(Guid Id);

    public class CreateVariantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/productVariants", async (CreateVariantRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateVariantCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateVariantResponse>();

                return Results.Created($"/productVariants/{response.Id}", response);
            })
            .WithName("CreateVariant")
            .Produces<CreateVariantResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("CreateVariant")
            .WithDescription("Create a product variant");

        }
    }
}
