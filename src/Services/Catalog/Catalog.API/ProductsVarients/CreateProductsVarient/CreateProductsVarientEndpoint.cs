namespace Catalog.API.Products.CreateProductsVariant
{
    public record CreateProductVariantRequest(string Name, string Description, Guid CategoryId);
    public record CreateProductVariantResponse(Guid Id);

    public class CreateProductVariantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductVariantRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductVariantCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductVariantResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProductVariant")
            .Produces<CreateProductVariantResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("CreateProductVariant")
            .WithDescription("Create a product variant");

        }
    }
}
