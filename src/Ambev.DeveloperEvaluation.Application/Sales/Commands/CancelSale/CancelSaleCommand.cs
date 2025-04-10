using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleCommand : IRequest<CancelSaleResult>
    {
        public Guid SaleId { get; set; }

        public CancelSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
