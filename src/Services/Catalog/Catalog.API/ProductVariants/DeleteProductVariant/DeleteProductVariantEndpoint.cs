﻿using Catalog.API.ProductVariants.DeleteProductVariant;

namespace Catalog.API.ProductVariants.DeleteProductsVariant
{
    public record DeleteProductsVariantRequest(Guid Id);
    public record DeleteProductsVariantResponse(bool IsSuccess);

    public class DeleteProductsVariantEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/productVariants/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductVariantCommand(id));

                var response = result.Adapt<DeleteProductsVariantResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteProductsVariant")
            .Produces<DeleteProductsVariantResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("DeleteProductsVariant")
            .WithDescription("Delete a product variant");
        }
    }
}
