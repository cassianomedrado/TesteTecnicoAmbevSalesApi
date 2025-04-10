using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do cliente deve ter no máximo 100 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$") 
                .WithMessage("O telefone informado não é válido.");

            RuleFor(x => x.Document)
                .NotEmpty().WithMessage("O documento do cliente é obrigatório.")
                .Must(BeValidCpfOrCnpj).WithMessage("Documento inválido. Deve ser um CPF ou CNPJ válido.");
        }

        private bool BeValidCpfOrCnpj(string document)
        {
            if (string.IsNullOrWhiteSpace(document)) return false;

            var cleaned = new string(document.Where(char.IsDigit).ToArray());

            // CPF: 11 dígitos, CNPJ: 14 dígitos
            return cleaned.Length == 11 || cleaned.Length == 14;
        }
    }
}
