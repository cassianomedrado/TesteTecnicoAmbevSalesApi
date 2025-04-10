using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Commands.DeleteBranch
{
    public class DeleteBranchCommand : IRequest<DeleteBranchResult>
    {
        public Guid Id { get; set; }

        public DeleteBranchCommand(Guid id)
        {
            Id = id;
        }
    }
}
