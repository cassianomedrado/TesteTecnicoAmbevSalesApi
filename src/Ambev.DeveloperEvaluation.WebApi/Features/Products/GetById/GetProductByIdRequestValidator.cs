using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetById
{
    public class GetProductByIdRequestValidator : AbstractValidator<GetProductByIdRequest>
    {
        public GetProductByIdRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("O Id do produto é obrigatório.")
                .Must(id => id != Guid.Empty).WithMessage("O Id do produto não pode ser um GUID vazio.");
        }
    }
}
