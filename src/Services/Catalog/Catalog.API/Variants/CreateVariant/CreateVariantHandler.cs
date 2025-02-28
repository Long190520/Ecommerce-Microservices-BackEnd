namespace Catalog.API.Variants.CreateVariant
{
    public record CreateVariantCommand(string Color, string Size, decimal Price, int StockQuantity, string ImageFile) : ICommand<CreateVariantResult>;
    public record CreateVariantResult(Guid Id);

    public class CreateVariantCommandValidator : AbstractValidator<CreateVariantCommand>
    {
        public CreateVariantCommandValidator()
        {
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color is required!");
            RuleFor(x => x.Size).NotEmpty().WithMessage("Size is required!");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Size is required").GreaterThan(0).WithMessage("Price must be greater than 0!");
            RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity is required").GreaterThan(0).WithMessage("StockQuantity must be greater than 0!");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required!");
        }
    }

    internal class CreateVariantHandler
        (IVariantRepository repository)
        : ICommandHandler<CreateVariantCommand, CreateVariantResult>
    {
        public async Task<CreateVariantResult> Handle(CreateVariantCommand command, CancellationToken cancellationToken)
        {
            var productVariant = new Variant
            {
                Id = new Guid(),
                Color = command.Color,
                Size= command.Size,
                Price = command.Price,
                StockQuantity = command.StockQuantity,
                ImageFile = command.ImageFile
            };

            var res = await repository.AddAsync(productVariant, cancellationToken);

            return new CreateVariantResult(productVariant.Id);
        }
    }
}
