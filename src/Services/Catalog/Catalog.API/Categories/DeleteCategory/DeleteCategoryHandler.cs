
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
        (IDocumentSession session)
        : ICommandHandler<DeleteCategoryCommand, DeleteCategoryResult>
    {
        public async Task<DeleteCategoryResult> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(request.Id, cancellationToken);

            if (category is null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            session.Delete(category);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteCategoryResult(true);
        }
    }
}
