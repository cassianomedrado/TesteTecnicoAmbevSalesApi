using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.ProcessSale
{
    public class ProcessSaleCommand : IRequest<ProcessSaleResult>
    {
        public Guid SaleId { get; set; }

        public ProcessSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
