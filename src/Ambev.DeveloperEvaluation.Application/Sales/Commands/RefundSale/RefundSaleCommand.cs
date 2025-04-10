using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.RefundSale
{
    public class RefundSaleCommand : IRequest<RefundSaleResult>
    {
        public Guid SaleId { get; set; }

        public RefundSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
