
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product Product);

    public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
        }
    }

    internal class GetProductByIdHandler
        (IProductRepository repository)
        : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await repository.GetProductById(query.Id, cancellationToken);

            return new GetProductByIdResult(product);
        }
    }
}
