namespace Catalog.API.ProductVariants.CreateProductVariant
{
    public record CreateProductVariantCommand(string Color, string Size, decimal Price, int StockQuantity, string ImageFile) : ICommand<CreateProductVariantResult>;
    public record CreateProductVariantResult(Guid Id);

    public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
    {
        public CreateProductVariantCommandValidator()
        {
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required!");
            RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Size is required").GreaterThan(0).WithMessage("Price must be greater than 0!");
            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity is required").GreaterThan(0).WithMessage("StockQuantity must be greater than 0!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required!");
        }
    }

    internal class CreateProductVariantHandler
        (IProductVariantRepository repository)
        : ICommandHandler<CreateProductVariantCommand, CreateProductVariantResult>
    {
        public async Task<CreateProductVariantResult> Handle(CreateProductVariantCommand command, CancellationToken cancellationToken)
        {
            var productVariant = new ProductVariant
            {
                Id = new Guid(),
                Color = command.Color,
                Size= command.Size,
                Price = command.Price,
                StockQuantity = command.StockQuantity,
                ImageFile = command.ImageFile
            };

            var res = await repository.AddAsync(productVariant, cancellationToken);

            return new CreateProductVariantResult(productVariant.Id);
        }
    }
}
