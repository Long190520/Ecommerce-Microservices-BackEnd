namespace Catalog.API.Products.CreateProductsVariant
{
    public record CreateProductVariantCommand(string Name, string Description, Guid CategoryId) : ICommand<CreateProductVariantResult>;
    public record CreateProductVariantResult(Guid Id);

    public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
    {
        public CreateProductVariantCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.CategoryId).NotEmpty().WithMessage("CategoryId is required");
        }
    }

    internal class CreateProductVariantHandler
        (IProductRepository repository)
        : ICommandHandler<CreateProductVariantCommand, CreateProductVariantResult>
    {
        public async Task<CreateProductVariantResult> Handle(CreateProductVariantCommand command, CancellationToken cancellationToken)
        {
            var productVariant = new ProductVariant
            {
                Id = new Guid()

            };

            var res = await repository.CreateAsync(command.Name, command.Description, command.CategoryId, cancellationToken);

            return new CreateProductVariantResult(productVariant.Id);
        }
    }
}
