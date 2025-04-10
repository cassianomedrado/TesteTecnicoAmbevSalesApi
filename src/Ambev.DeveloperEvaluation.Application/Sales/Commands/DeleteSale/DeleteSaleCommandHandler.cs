using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _saleRepository.DeleteAsync(command.SaleId, cancellationToken);

                return new DeleteSaleResult
                {
                    Success = deleted,
                    Message = deleted ? "Venda excluída com sucesso." : "Venda não encontrada."
                };
            }
            catch (Exception ex)
            {
                return new DeleteSaleResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
