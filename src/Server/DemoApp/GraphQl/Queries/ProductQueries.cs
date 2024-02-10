using GraphQlProductsDemo.Data;
using GraphQlProductsDemo.GraphQl.Utilities;

namespace GraphQlProductsDemo.GraphQl.Queries
{
    [ExtendObjectType(Constants.QueryType)]
    public class ProductQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts(AppDbContext context)
        {
            return context.Products;
        }
    }
}
