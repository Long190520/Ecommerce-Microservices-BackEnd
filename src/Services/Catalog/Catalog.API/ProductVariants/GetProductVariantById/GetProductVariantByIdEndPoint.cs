namespace Catalog.API.ProductVariants.GetProductById
{
    public record GetProductVariantByIdRequest(Guid id);
    public record GetProductVariantByIdResponse(ProductVariant ProductVariant);

    public class GetProductVariantByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/productVariants/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductVariantByIdQuery(id));

                var response = result.Adapt<GetProductVariantByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductVariantById")
            .Produces<GetProductVariantByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("GetProductVariantById")
            .WithDescription("Get Product Variant By Id");
        }
    }
}
