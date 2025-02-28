namespace Catalog.API.Variants.GetVariantById
{
    public record GetVariantByIdRequest(Guid id);
    public record GetVariantByIdResponse(Variant Variant);

    public class GetVariantByIdEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/productVariants/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetVariantByIdQuery(id));

                var response = result.Adapt<GetVariantByIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetVariantById")
            .Produces<GetVariantByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("GetVariantById")
            .WithDescription("Get Product Variant By Id");
        }
    }
}
