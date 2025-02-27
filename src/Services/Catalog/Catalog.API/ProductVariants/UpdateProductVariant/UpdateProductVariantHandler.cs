using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductVariantCommand(Guid Id, string Color, string Size, decimal Price, int StockQuantity, string ImageFile) : ICommand<UpdateProductVariantResult>;
    public record UpdateProductVariantResult(ProductVariant ProductVariant);

    public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
    {
        public UpdateProductVariantCommandValidator()
        {
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required!");
            RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Size is required").GreaterThan(0).WithMessage("Price must be greater than 0!");
            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity is required").GreaterThan(0).WithMessage("StockQuantity must be greater than 0!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required!");
        }
    }

    internal class UpdateProductVariantHandler
        (IProductVariantRepository repository)
        : ICommandHandler<UpdateProductVariantCommand, UpdateProductVariantResult>
    {
        public async Task<UpdateProductVariantResult> Handle(UpdateProductVariantCommand command, CancellationToken cancellationToken)
        {
            var productVariant = new ProductVariant
            {
                Id = command.Id,
                Color = command.Color,
                Size = command.Size,
                Price = command.Price,
                StockQuantity = command.StockQuantity,
                ImageFile = command.ImageFile
            };

            var result = await repository.UpdateAsync(productVariant, cancellationToken);

            return new UpdateProductVariantResult(result);
        }
    }
}
