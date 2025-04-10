using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class BranchValidator : AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("O nome da filial é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da filial deve ter no máximo 100 caracteres.");

            RuleFor(b => b.Address)
                .NotEmpty().WithMessage("O endereço da filial é obrigatório.")
                .MaximumLength(200).WithMessage("O endereço da filial deve ter no máximo 200 caracteres.");
        }
    }
}
