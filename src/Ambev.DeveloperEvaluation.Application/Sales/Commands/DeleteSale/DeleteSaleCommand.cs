using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommand : IRequest<DeleteSaleResult>
    {
        public Guid SaleId { get; set; }

        public DeleteSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
