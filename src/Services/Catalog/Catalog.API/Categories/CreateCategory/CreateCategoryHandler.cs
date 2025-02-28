
using Catalog.API.Data.CategoryData;

namespace Catalog.API.Categories.CreateCategory
{
    public record CreateCategoryCommand(string Name) : ICommand<CreateCategoryResult>;
    public record CreateCategoryResult(Guid Id);

    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Category Name canot be null!");
        }
    }

    public class UpdateCategoryHandler
        (ICategoryRepository repository)
        : ICommandHandler<CreateCategoryCommand, CreateCategoryResult>
    {
        public async Task<CreateCategoryResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name
            };

            var res = await repository.AddAsync(category, cancellationToken);

            return new CreateCategoryResult(res.Id);
        }
    }
}
