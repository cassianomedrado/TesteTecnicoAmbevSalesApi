using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
    {
        public GetSaleByIdRequestValidator()
        {
            RuleFor(request => request.Id)
                .NotEmpty().WithMessage("O Id da venda é obrigatório.")
                .Must(id => id != Guid.Empty).WithMessage("O Id da venda não pode ser um GUID vazio.");
        }
    }
}
