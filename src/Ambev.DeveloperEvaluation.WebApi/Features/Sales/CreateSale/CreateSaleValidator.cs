using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemValidator());
        }
    }

    public class CreateSaleItemValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty();
            RuleFor(x => x.Quantity).GreaterThan(0);
            RuleFor(x => x.UnitPrice).GreaterThan(0);
        }
    }
}
