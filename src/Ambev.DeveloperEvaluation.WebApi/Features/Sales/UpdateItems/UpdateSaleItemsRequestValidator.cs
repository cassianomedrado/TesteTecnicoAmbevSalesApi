using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateItems
{
    public class UpdateSaleItemsRequestValidator : AbstractValidator<UpdateSaleItemsRequest>
    {
        public UpdateSaleItemsRequestValidator()
        {
            RuleFor(request => request.Items)
                .NotEmpty().WithMessage("A lista de itens não pode estar vazia.");

            RuleForEach(request => request.Items).ChildRules(items =>
            {
                items.RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("O Id do item é obrigatório.");

                items.RuleFor(item => item.ProductId)
                    .NotEmpty().WithMessage("O ID do produto é obrigatório.");

                items.RuleFor(item => item.Quantity)
                    .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

                items.RuleFor(item => item.UnitPrice)
                    .GreaterThanOrEqualTo(0).WithMessage("O valor unitário deve ser maior que zero.");
            });
        }
    }
}
