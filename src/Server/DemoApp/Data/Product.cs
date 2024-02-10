using System.ComponentModel.DataAnnotations;

namespace GraphQlProductsDemo.Data
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
