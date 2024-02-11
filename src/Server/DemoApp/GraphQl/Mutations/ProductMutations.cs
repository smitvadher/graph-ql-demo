using GraphQlProductsDemo.Data;
using GraphQlProductsDemo.GraphQl.Inputs;
using GraphQlProductsDemo.GraphQl.Utilities;

namespace GraphQlProductsDemo.GraphQl.Mutations
{
    [ExtendObjectType(Constants.MutationType)]
    public class ProductMutations
    {
        public async Task<int> CreateProduct(
            AppDbContext context,
            ProductInput input)
        {
            var product = new Product
            {
                Name = input.Name,
                Description = input.Description,
                Price = input.Price,
                IsAvailable = input.IsAvailable,
                CategoryId = input.CategoryId
            };

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<bool> DeleteProduct(
            AppDbContext context,
            int productId)
        {
            var product = await context.Products.FindAsync(productId);

            if (product == null)
            {
                return false;
            }

            context.Products.Remove(product);
            await context.SaveChangesAsync();

            // Product deleted successfully
            return true;
        }
    }
}
