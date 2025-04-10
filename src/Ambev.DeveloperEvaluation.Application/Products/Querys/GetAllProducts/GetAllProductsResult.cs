namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetAllProducts
{
    public class GetAllProductsResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
