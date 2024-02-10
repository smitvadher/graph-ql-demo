namespace GraphQlProductsDemo.Data
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        [UseFiltering]
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
