namespace GraphQlProductsDemo.GraphQl.Inputs
{
    public record ProductInput(string Name, string Description, decimal Price, bool IsAvailable, int CategoryId);
}
