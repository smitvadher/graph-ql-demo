using Bogus;
using Microsoft.EntityFrameworkCore;

namespace GraphQlProductsDemo.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext _context;

        public DataSeeder(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            if (await _context.Categories.AnyAsync())
                return;

            Randomizer.Seed = new Random(13);

            var categories = new Faker<Category>()
                .RuleFor(c => c.Name, f => f.Commerce.Categories(1)[0])
                .RuleFor(c => c.Description, f => f.Lorem.Paragraph())
                .RuleFor(c => c.ImageUrl, f => f.Image.PicsumUrl())
                .Generate(10);
            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();

            var products = new Faker<Product>()
                .RuleFor(p => p.CategoryId, f => f.PickRandom(categories).Id)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => Convert.ToDecimal(f.Commerce.Price()))
                .RuleFor(p => p.IsAvailable, f => f.PickRandom(true, false))
                .Generate(100);
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            var images = new Faker<Image>()
                .RuleFor(i => i.ProductId, f => f.PickRandom(products).Id)
                .RuleFor(i => i.Url, f => f.Image.PicsumUrl())
                .RuleFor(i => i.Size, f => f.PickRandom<ImageSize>())
                .Generate(100);
            await _context.Images.AddRangeAsync(images);
            await _context.SaveChangesAsync();
        }
    }
}
