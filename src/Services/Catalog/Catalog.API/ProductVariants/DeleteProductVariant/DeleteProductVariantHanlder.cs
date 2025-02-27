namespace Catalog.API.ProductVariants.DeleteProductVariant
{
    public record DeleteProductVariantCommand(Guid Id) : ICommand<DeleteProductVariantResult>;
    public record DeleteProductVariantResult(bool IsSuccess);

    public class DeleteProductVariantCommandValidator : AbstractValidator<DeleteProductVariantCommand>
    {
        public DeleteProductVariantCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product variant Id is required");
        }
    }

    internal class DeleteProductVariantHanlder
        (IProductVariantRepository repository)
        : ICommandHandler<DeleteProductVariantCommand, DeleteProductVariantResult>
    {
        public async Task<DeleteProductVariantResult> Handle(DeleteProductVariantCommand command, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(command.Id, cancellationToken);

            return new DeleteProductVariantResult(result);
        }
    }
}
