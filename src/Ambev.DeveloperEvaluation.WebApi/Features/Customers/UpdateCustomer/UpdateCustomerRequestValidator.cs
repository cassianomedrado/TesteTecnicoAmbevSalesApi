using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer
{
    public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
    {
        public UpdateCustomerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(?\d{2}\)?[\s\-]?\d{4,5}\-?\d{4}$")
                .WithMessage("O telefone informado não é válido.");

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage("O documento é obrigatório.")
                .Length(11).WithMessage("O documento deve conter 11 dígitos (CPF).")
                .Matches(@"^\d{11}$").WithMessage("O documento deve conter apenas números.");
        }
    }
}
