using GraphQlProductsDemo.Data;
using GraphQlProductsDemo.GraphQl.Inputs;
using GraphQlProductsDemo.GraphQl.Utilities;

namespace GraphQlProductsDemo.GraphQl.Mutations
{
    [ExtendObjectType(Constants.MutationType)]
    public class CategoryMutations
    {
        public async Task<int> CreateCategory(
            AppDbContext context,
            CategoryInput input)
        {
            var category = new Category
            {
                Name = input.Name,
                Description = input.Description,
                ImageUrl = input.ImageUrl
            };

            context.Categories.Add(category);
            await context.SaveChangesAsync();

            return category.Id;
        }

        public async Task<bool> DeleteCategory(
            AppDbContext context,
            int categoryId)
        {
            var category = await context.Categories.FindAsync(categoryId);

            if (category == null)
                return false;

            context.Categories.Remove(category);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
