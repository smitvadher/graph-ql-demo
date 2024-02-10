using GraphQlProductsDemo.Data;
using GraphQlProductsDemo.GraphQl.Utilities;

namespace GraphQlProductsDemo.GraphQl.Queries
{
    [ExtendObjectType(Constants.QueryType)]
    public class CategoryQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Category> GetCategories(AppDbContext context)
        {
            return context.Categories;
        }
    }
}
