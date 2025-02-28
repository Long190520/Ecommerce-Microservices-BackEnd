namespace Catalog.API.Variants.DeleteVariant
{
    public record DeleteVariantCommand(Guid Id) : ICommand<DeleteVariantResult>;
    public record DeleteVariantResult(bool IsSuccess);

    public class DeleteVariantCommandValidator : AbstractValidator<DeleteVariantCommand>
    {
        public DeleteVariantCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product variant Id is required");
        }
    }

    internal class DeleteVariantHanlder
        (IVariantRepository repository)
        : ICommandHandler<DeleteVariantCommand, DeleteVariantResult>
    {
        public async Task<DeleteVariantResult> Handle(DeleteVariantCommand command, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteAsync(command.Id, cancellationToken);

            return new DeleteVariantResult(result);
        }
    }
}
