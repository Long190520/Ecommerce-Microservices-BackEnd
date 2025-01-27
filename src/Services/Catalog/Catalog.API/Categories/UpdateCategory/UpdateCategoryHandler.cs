
using Catalog.API.Categories.DeleteCategory;

namespace Catalog.API.Categories.UpdateCategory
{
    public record UpdateCategoryCommand(Guid Id, string Name) : ICommand<UpdateCategoryResult>;
    public record UpdateCategoryResult(bool IsSuccess);

    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Category Id canot be null!");
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Category Name canot be null or empty!");
        }
    }

    public class DeleteCategoryHandler
        (IDocumentSession session)
        : ICommandHandler<UpdateCategoryCommand, UpdateCategoryResult>
    {
        public async Task<UpdateCategoryResult> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(request.Id, cancellationToken);

            if (category is null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            category.Name = request.Name;

            session.Update(category);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateCategoryResult(true);
        }
    }
}
