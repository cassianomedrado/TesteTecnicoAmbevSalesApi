using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetById;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetById
{
    public class GetBranchByIdRequestValidator : AbstractValidator<GetBranchByIdRequest>
    {
        public GetBranchByIdRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("O Id da filial é obrigatória.")
                .Must(id => id != Guid.Empty).WithMessage("O Id da filial não pode ser um GUID vazio.");
        }
    }
}
