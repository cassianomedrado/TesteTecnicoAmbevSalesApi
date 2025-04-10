using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CompleteSale
{
    public class CompleteSaleCommandHandler : IRequestHandler<CompleteSaleCommand, CompleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public CompleteSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CompleteSaleResult> Handle(CompleteSaleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
                if (sale == null)
                {
                    return new CompleteSaleResult
                    {
                        Success = false,
                        Message = "Venda não encontrada."
                    };
                }
      
                sale.Complete();
                await _saleRepository.UpdateAsync(sale, cancellationToken);

                return new CompleteSaleResult
                {
                    Success = true,
                    Message = "Venda finalizada com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new CompleteSaleResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
