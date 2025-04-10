using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(s => s.SaleNumber)
                .NotEmpty().WithMessage("O número da venda é obrigatório.");

            RuleFor(s => s.SaleDate)
                .NotEmpty().WithMessage("A data da venda é obrigatória.");

            RuleFor(s => s.CustomerId)
                .NotEqual(Guid.Empty).WithMessage("O cliente é obrigatório.");

            RuleFor(s => s.BranchId)
                .NotEqual(Guid.Empty).WithMessage("A filial é obrigatória.");

            RuleFor(s => s.Items)
                .NotEmpty().WithMessage("A venda deve conter pelo menos um item.");

            RuleForEach(s => s.Items).SetValidator(new SaleItemValidator());

            RuleFor(s => s.TotalValue)
                .GreaterThan(0).WithMessage("O valor total da venda deve ser maior que zero.");

            RuleFor(s => s.Status)
                .IsInEnum().WithMessage("Status da venda inválido.");
        }
    }
}