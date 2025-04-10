using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var sale = new Sale(request.SaleNumber, request.SaleDate, request.CustomerId, request.BranchId, _mediator);

                foreach (var itemDto in request.Items)
                {
                    var item = new SaleItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice);
                    sale.AddItem(item);
                }

                var validation = sale.Validate();
                if (!validation.IsValid)
                {
                    var errors = validation.Errors
                        .Select(e => $"{e.Error}: {e.Detail}");

                    throw new ApplicationException("Erro de validação na venda: " + string.Join(" | ", errors));
                }

                var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

                return new CreateSaleResult
                {
                    SaleId = createdSale.Id,
                    SaleNumber = createdSale.SaleNumber,
                    SaleDate = createdSale.SaleDate,
                    TotalValue = createdSale.TotalValue
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao criar a venda: " + ex.Message);   
            }
        }
    }
}
