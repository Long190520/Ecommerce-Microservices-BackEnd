namespace Catalog.API.ProductVariants.GetProductVariantsByProductId
{
    public record GetProductVariantsByProductIdQuery(Guid productId) : IQuery<GetProductVariantsByProductIdResult>;
    public record GetProductVariantsByProductIdResult(IReadOnlyList<ProductVariant> ProductVariants);

    internal class GetProductVariantsByProductIdHandler
        (IProductVariantRepository repository)
        : IQueryHandler<GetProductVariantsByProductIdQuery, GetProductVariantsByProductIdResult>
    {
        public async Task<GetProductVariantsByProductIdResult> Handle(GetProductVariantsByProductIdQuery query, CancellationToken cancellationToken)
        {
            var productVariants = await repository.GetProductVariantsByProductId(query.productId, cancellationToken);

            return new GetProductVariantsByProductIdResult(productVariants);
        }
    }
}
