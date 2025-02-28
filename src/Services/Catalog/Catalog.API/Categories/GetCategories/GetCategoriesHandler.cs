using Catalog.API.Data.CategoryData;

namespace Catalog.API.Categories.GetCategories
{
    public record GetCategoriesQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetCategoriesResult>;
    public record GetCategoriesResult(IEnumerable<Category> Categories);

    internal class GetCategoriesHandler
        (ICategoryRepository repository)
        : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var categories = await repository.GetPagingAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);

            return new GetCategoriesResult(categories);
        }
    }
}
