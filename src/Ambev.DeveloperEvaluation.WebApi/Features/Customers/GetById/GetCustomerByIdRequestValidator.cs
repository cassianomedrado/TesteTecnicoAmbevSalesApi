using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetById
{
    public class GetCustomerByIdRequestValidator : AbstractValidator<GetCustomerByIdRequest>
    {
        public GetCustomerByIdRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("O Id do cliente é obrigatório.")
                .Must(id => id != Guid.Empty).WithMessage("O Id do cliente não pode ser um GUID vazio.");
        }
    }
}
