namespace Catalog.API.Variants.GetVariantById
{
    public record GetVariantByIdQuery(Guid Id) : IQuery<GetVariantByIdResult>;
    public record GetVariantByIdResult(Variant Variant);

    public class GetVariantByIdRequestValidator : AbstractValidator<GetVariantByIdQuery>
    {
        public GetVariantByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }

    internal class GetVariantByIdHandler
        (IVariantRepository repository)
        : IQueryHandler<GetVariantByIdQuery, GetVariantByIdResult>
    {
        public async Task<GetVariantByIdResult> Handle(GetVariantByIdQuery query, CancellationToken cancellationToken)
        {
            var productVariant = await repository.GetByIdAsync(query.Id, cancellationToken);

            return new GetVariantByIdResult(productVariant);
        }
    }
}
