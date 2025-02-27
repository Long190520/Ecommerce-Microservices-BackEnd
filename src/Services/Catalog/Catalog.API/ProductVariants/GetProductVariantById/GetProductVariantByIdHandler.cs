namespace Catalog.API.ProductVariants.GetProductById
{
    public record GetProductVariantByIdQuery(Guid Id) : IQuery<GetProductVariantByIdResult>;
    public record GetProductVariantByIdResult(ProductVariant ProductVariant);

    public class GetProductVariantByIdRequestValidator : AbstractValidator<GetProductVariantByIdQuery>
    {
        public GetProductVariantByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }

    internal class GetProductVariantByIdHandler
        (IProductVariantRepository repository)
        : IQueryHandler<GetProductVariantByIdQuery, GetProductVariantByIdResult>
    {
        public async Task<GetProductVariantByIdResult> Handle(GetProductVariantByIdQuery query, CancellationToken cancellationToken)
        {
            var productVariant = await repository.GetByIdAsync(query.Id, cancellationToken);

            return new GetProductVariantByIdResult(productVariant);
        }
    }
}
