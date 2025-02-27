using Catalog.API.Models;
using Catalog.API.ProductVariants.GetProductVariantsByProductId;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductVariantsByProductIdRequest(Guid Id);
    public record GetProductVariantsByProductIdResponse(IReadOnlyList<ProductVariant> ProductVariants);

    public class GetProductVariantsByProductIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/productVarients/product/{productId}", async (Guid productId, ISender sender) =>
            {
                var result = await sender.Send(new GetProductVariantsByProductIdQuery(productId));

                var response = result.Adapt<GetProductVariantsByProductIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProductVariantsByProductId")
            .Produces<GetProductVariantsByProductIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetProductVariantsByProductId")
            .WithDescription("Get Product Variants By Product Id");
        }
    }
}
