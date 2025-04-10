namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetById
{
    public class GetProductByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
