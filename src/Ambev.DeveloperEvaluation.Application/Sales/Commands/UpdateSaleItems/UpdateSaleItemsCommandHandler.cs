using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSaleItems
{
    public class UpdateSaleItemsCommandHandler : IRequestHandler<UpdateSaleItemsCommand, UpdateSaleItemsResult>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleItemsCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<UpdateSaleItemsResult> Handle(UpdateSaleItemsCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

                if (sale == null)
                    return new UpdateSaleItemsResult
                    {
                        Success = false,
                        Message = "Venda não encontrada."
                    };      

                foreach (var item in command.Items)
                {
                    sale.UpdateItem(item.Id, item.Quantity, item.UnitPrice);
                }

                var validation = sale.Validate();
                if (!validation.IsValid)
                {
                    var errors = validation.Errors
                        .Select(e => $"{e.Error}: {e.Detail}");

                    return new UpdateSaleItemsResult
                    {
                        Success = false,
                        Message = "Erro de validação na venda: " + string.Join(" | ", errors)
                    };
                }

                await _saleRepository.UpdateAsync(sale, cancellationToken);
                return new UpdateSaleItemsResult
                {
                    Success = true,
                    Message = "Items atualizados com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new UpdateSaleItemsResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }           
        }
    }
}
