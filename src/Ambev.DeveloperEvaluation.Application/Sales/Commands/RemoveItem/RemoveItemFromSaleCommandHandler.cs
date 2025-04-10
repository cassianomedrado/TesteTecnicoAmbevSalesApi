using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.RemoverItem
{
    public class RemoveItemFromSaleCommandHandler : IRequestHandler<RemoveItemFromSaleCommand, RemoveItemFromSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        public RemoveItemFromSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<RemoveItemFromSaleResult> Handle(RemoveItemFromSaleCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

                if (sale == null)
                {
                    return new RemoveItemFromSaleResult
                    {
                        Success = false,
                        Message = "Venda não encontrada."
                    };
                }

                var item = sale.Items.FirstOrDefault(i => i.Id == command.ItemId);
                if (item == null)
                {
                    return new RemoveItemFromSaleResult
                    {
                        Success = false,
                        Message = "Item não encontrado na venda."
                    };
                }

                sale.SetMediator(_mediator);
                sale.RemoveItem(item);

                await _saleRepository.UpdateAsync(sale, cancellationToken);

                return new RemoveItemFromSaleResult
                {
                    Success = true,
                    Message = "Item removido com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new RemoveItemFromSaleResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
