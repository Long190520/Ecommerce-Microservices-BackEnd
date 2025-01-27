namespace Catalog.API.Categories.GetCategories
{
    public record GetCategoriesQuery(int? PageNumer = 1, int? PageSize = 10) : IQuery<GetCategoriesResult>;
    public record GetCategoriesResult(IEnumerable<Category> Categories);

    internal class GetCategoriesHandler
        (IDocumentSession session)
        : IQueryHandler<GetCategoriesQuery, GetCategoriesResult>
    {
        public async Task<GetCategoriesResult> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            var Categories = await session.Query<Category>()
                .ToPagedListAsync(query.PageNumer ?? 1, query.PageSize ?? 1, cancellationToken);

            return new GetCategoriesResult(Categories);
        }
    }
}
