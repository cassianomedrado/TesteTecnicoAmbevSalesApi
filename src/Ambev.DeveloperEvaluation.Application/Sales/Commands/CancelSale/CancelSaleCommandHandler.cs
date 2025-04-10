using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        public CancelSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);
                if (sale == null)
                {
                    return new CancelSaleResult
                    {
                        Success = false,
                        Message = "Venda não encontrada."
                    };
                }

                sale.SetMediator(_mediator);
                sale.Cancel();
                await _saleRepository.UpdateAsync(sale, cancellationToken);

                return new CancelSaleResult
                {
                    Success = true,
                    Message = "Venda cancelada com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new CancelSaleResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
