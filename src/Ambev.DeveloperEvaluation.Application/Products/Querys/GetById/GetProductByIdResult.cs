namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetById
{
    public class GetProductByIdResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
