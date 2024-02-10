using System.ComponentModel.DataAnnotations;

namespace GraphQlProductsDemo.Data
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
