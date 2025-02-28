using Catalog.API.Models;
using Catalog.API.Variants.GetVariantsByProductId;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetVariantsByProductIdRequest(Guid Id);
    public record GetVariantsByProductIdResponse(IReadOnlyList<Variant> Variants);

    public class GetVariantsByProductIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/productVarients/product/{productId}", async (Guid productId, ISender sender) =>
            {
                var result = await sender.Send(new GetVariantsByProductIdQuery(productId));

                var response = result.Adapt<GetVariantsByProductIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetVariantsByProductId")
            .Produces<GetVariantsByProductIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("GetVariantsByProductId")
            .WithDescription("Get Product Variants By Product Id");
        }
    }
}
