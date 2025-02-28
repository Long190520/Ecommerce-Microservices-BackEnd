namespace Catalog.API.Variants.GetVariantsByProductId
{
    public record GetVariantsByProductIdQuery(Guid productId) : IQuery<GetVariantsByProductIdResult>;
    public record GetVariantsByProductIdResult(IReadOnlyList<Variant> Variants);

    internal class GetVariantsByProductIdHandler
        (IVariantRepository repository)
        : IQueryHandler<GetVariantsByProductIdQuery, GetVariantsByProductIdResult>
    {
        public async Task<GetVariantsByProductIdResult> Handle(GetVariantsByProductIdQuery query, CancellationToken cancellationToken)
        {
            var productVariants = await repository.GetVariantsByProductId(query.productId, cancellationToken);

            return new GetVariantsByProductIdResult(productVariants);
        }
    }
}
