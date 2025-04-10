using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Valida os dados de um produto.
/// </summary>
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");

        RuleFor(p => p.Description)
            .MaximumLength(500).WithMessage("A descrição do produto deve ter no máximo 500 caracteres.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("O preço do produto deve ser maior que zero.");

    }
}
