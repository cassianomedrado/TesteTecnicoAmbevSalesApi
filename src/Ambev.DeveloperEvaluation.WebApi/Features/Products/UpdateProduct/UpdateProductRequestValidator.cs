using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("O nome do produto é obrigatório.")
                .MaximumLength(100).WithMessage("O nome do produto deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("A descrição do produto é obrigatória.")
                .MaximumLength(500).WithMessage("A descrição deve ter no máximo 500 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("O preço deve ser maior que zero.");
        }
    }
}
