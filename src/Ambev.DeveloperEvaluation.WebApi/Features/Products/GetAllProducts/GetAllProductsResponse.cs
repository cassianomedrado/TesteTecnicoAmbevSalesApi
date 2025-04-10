namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
    public class GetAllProductsResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
