using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CompleteSale
{
    public class CompleteSaleCommand : IRequest<CompleteSaleResult>
    {
        public Guid SaleId { get; set; }

        public CompleteSaleCommand(Guid saleId)
        {
            SaleId = saleId;
        }
    }
}
