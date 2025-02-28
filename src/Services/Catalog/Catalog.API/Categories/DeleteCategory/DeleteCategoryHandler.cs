
using Catalog.API.Data.CategoryData;

namespace Catalog.API.Categories.DeleteCategory
{
    public record DeleteCategoryCommand(Guid Id) : ICommand<DeleteCategoryResult>;
    public record DeleteCategoryResult(bool IsSuccess);

    public class DeleteCategoryCommandValidator : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category Id canot be null!");
        }
    }

    public class DeleteCategoryHandler
        (ICategoryRepository repository
        : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var res = await repository.DeleteAsync(command.Id, cancellationToken);

            return new DeleteCategoryResult(res);
        }
    }
}
