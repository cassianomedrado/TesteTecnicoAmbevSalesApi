using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetAllBranchs
{
    public class GetAllBranchesQuery : IRequest<List<GetAllBranchsResult>> { }
}
