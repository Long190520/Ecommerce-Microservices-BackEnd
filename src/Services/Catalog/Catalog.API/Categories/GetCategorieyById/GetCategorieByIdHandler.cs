using Catalog.API.Data.CategoryData;
using Catalog.API.Models;

namespace Catalog.API.Categories.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid categoryId) : IQuery<GetCategoryByIdResult>;
    public record GetCategoryByIdResult(Category Category);

    internal class GetCategoryByIdHandler
        (ICategoryRepository repository)
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
    {
        public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(query.categoryId, cancellationToken);

            return new GetCategoryByIdResult(category);
        }
    }
}
