namespace GraphQlProductsDemo.Data
{
    public class Image : BaseEntity
    {
        public string Url { get; set; }

        public ImageSize Size { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }

    public enum ImageSize
    {
        Small,
        Medium,
        Large
    }
}
