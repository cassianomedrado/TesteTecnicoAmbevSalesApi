using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetById
{
    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, GetBranchByIdResult>
    {
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetBranchByIdQueryHandler(IBranchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetBranchByIdResult> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _repository.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<GetBranchByIdResult>(branch);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao buscar filial: " + ex.Message);
            }
        }
    }
}
