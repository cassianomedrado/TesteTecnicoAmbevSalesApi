namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleResult
    {
        public Guid SaleId { get; set; }
        public string SaleNumber { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
