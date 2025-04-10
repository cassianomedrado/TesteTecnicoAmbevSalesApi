using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.RefundSale
{
    public class RefundSaleCommandHandler : IRequestHandler<RefundSaleCommand, RefundSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public RefundSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<RefundSaleResult> Handle(RefundSaleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

                if (sale == null)
                {
                    return new RefundSaleResult
                    {
                        Success = false,
                        Message = "Venda não encontrada."
                    };
                }

                sale.Refund();
                await _saleRepository.UpdateAsync(sale, cancellationToken);

                return new RefundSaleResult
                {
                    Success = true,
                    Message = "Venda reembolsada com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new RefundSaleResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
