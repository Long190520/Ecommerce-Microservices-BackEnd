using Catalog.API.Models;

namespace Catalog.API.Categories.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid categoryId) : IQuery<GetCategoryByIdResult>;
    public record GetCategoryByIdResult(Category Category);

    internal class GetCategoryByIdHandler
        (IDocumentSession session)
        : IQueryHandler<GetCategoryByIdQuery, GetCategoryByIdResult>
    {
        public async Task<GetCategoryByIdResult> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await session.LoadAsync<Category>(query.categoryId,cancellationToken);

            return new GetCategoryByIdResult(category);
        }
    }
}
