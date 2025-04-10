using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSaleItems
{
    public class UpdateSaleItemsCommand : IRequest<UpdateSaleItemsResult>
    {
        public Guid SaleId { get; set; }
        public List<UpdateSaleItemCommand> Items { get; set; } = new();
    }

    public class UpdateSaleItemCommand
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
