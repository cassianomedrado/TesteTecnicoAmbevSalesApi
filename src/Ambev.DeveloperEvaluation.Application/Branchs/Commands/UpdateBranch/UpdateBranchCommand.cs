using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Commands.UpdateBranch
{
    public class UpdateBranchCommand : IRequest<UpdateBranchResult>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
