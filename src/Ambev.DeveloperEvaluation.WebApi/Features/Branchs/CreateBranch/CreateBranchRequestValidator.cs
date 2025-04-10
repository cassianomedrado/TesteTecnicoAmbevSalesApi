using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch
{
    public class CreateBranchRequestValidator : AbstractValidator<CreateBranchRequest>
    {
        public CreateBranchRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome da filial é obrigatório.")
                .MaximumLength(100).WithMessage("O nome da filial deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("O endereço é obrigatório.")
                .MaximumLength(200).WithMessage("O endereço deve ter no máximo 200 caracteres.");
        }
    }
}
