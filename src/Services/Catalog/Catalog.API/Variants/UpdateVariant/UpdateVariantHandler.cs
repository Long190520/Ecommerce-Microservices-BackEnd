namespace Catalog.API.Products.UpdateVariant
{
    public record UpdateVariantCommand(Guid Id, string Color, string Size, decimal Price, int StockQuantity, string ImageFile) : ICommand<UpdateVariantResult>;
    public record UpdateVariantResult(Variant Variant);

    public class UpdateVariantCommandValidator : AbstractValidator<UpdateVariantCommand>
    {
        public UpdateVariantCommandValidator()
        {
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required!");
            RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Size is required").GreaterThan(0).WithMessage("Price must be greater than 0!");
            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity is required").GreaterThan(0).WithMessage("StockQuantity must be greater than 0!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required!");
        }
    }

    internal class UpdateVariantHandler
        (IVariantRepository repository)
        : ICommandHandler<UpdateVariantCommand, UpdateVariantResult>
    {
        public async Task<UpdateVariantResult> Handle(UpdateVariantCommand command, CancellationToken cancellationToken)
        {
            var productVariant = new Variant
            {
                Id = command.Id,
                Color = command.Color,
                Size = command.Size,
                Price = command.Price,
                StockQuantity = command.StockQuantity,
                ImageFile = command.ImageFile
            };

            var result = await repository.UpdateAsync(productVariant, cancellationToken);

            return new UpdateVariantResult(result);
        }
    }
}
