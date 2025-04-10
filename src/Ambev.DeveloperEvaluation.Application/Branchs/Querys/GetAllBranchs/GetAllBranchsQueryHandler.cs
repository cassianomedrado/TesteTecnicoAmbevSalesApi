using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetAllBranchs
{
    public class GetAllBranchesQueryHandler : IRequestHandler<GetAllBranchesQuery, List<GetAllBranchsResult>>
    {
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBranchesQueryHandler(IBranchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllBranchsResult>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var branches = await _repository.GetAllAsync(cancellationToken);
                return branches.Select(_mapper.Map<GetAllBranchsResult>).ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao buscar filiais: " + ex.Message);
            }
        }
    }
}
