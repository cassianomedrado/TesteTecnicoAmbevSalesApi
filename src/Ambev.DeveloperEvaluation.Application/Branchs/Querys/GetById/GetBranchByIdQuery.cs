using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetById
{
    public class GetBranchByIdQuery : IRequest<GetBranchByIdResult>
    {
        public Guid Id { get; set; }

        public GetBranchByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
