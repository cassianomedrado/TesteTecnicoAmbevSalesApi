namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateItems
{
    public class UpdateSaleItemsRequest
    {
        public List<UpdateSaleItemRequest> Items { get; set; } = new();
    }

    public class UpdateSaleItemRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
