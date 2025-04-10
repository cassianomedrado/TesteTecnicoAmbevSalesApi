using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.UpdateBranch
{
    public class UpdateBranchRequestValidator : AbstractValidator<UpdateBranchRequest>
    {
        public UpdateBranchRequestValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.Name));

            RuleFor(x => x.Address)
                .MaximumLength(200).When(x => !string.IsNullOrWhiteSpace(x.Address));
        }
    }
}
