using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.RemoverItem
{
    public class RemoveItemFromSaleCommand : IRequest<RemoveItemFromSaleResult>
    {
        public Guid SaleId { get; set; }
        public Guid ItemId { get; set; }

        public RemoveItemFromSaleCommand(Guid saleId, Guid itemId)
        {
            SaleId = saleId;
            ItemId = itemId;
        }
    }
}
