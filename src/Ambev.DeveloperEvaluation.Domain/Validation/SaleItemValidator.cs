using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(i => i.ProductId)
                .NotEqual(Guid.Empty).WithMessage("Produto é obrigatório.");

            RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");

            RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");

            RuleFor(i => i.TotalPrice)
                .GreaterThan(0).WithMessage("O preço total deve ser maior que zero.");
        }
    }
}