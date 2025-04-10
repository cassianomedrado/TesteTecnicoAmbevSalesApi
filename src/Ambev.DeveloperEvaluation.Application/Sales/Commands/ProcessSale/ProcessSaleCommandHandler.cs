using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.ProcessSale
{
    public class ProcessSaleCommandHandler : IRequestHandler<ProcessSaleCommand, ProcessSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public ProcessSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<ProcessSaleResult> Handle(ProcessSaleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

                if (sale == null)
                {
                    return new ProcessSaleResult
                    {
                        Success = false,
                        Message = "Venda não encontrada."
                    };
                }

                sale.Process();
                await _saleRepository.UpdateAsync(sale, cancellationToken);

                return new ProcessSaleResult
                {
                    Success = true,
                    Message = "Venda processada com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new ProcessSaleResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
