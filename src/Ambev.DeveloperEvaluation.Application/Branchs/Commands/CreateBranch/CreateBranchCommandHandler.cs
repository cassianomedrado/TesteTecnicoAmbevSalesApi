using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Commands.CreateBranch
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreateBranchResult>
    {
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public CreateBranchCommandHandler(IBranchRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateBranchResult> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = new Branch(request.Name, request.Address);
                await _repository.CreateAsync(branch, cancellationToken);

                return _mapper.Map<CreateBranchResult>(branch);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao criar filial: " + ex.Message);
            }
        }
    }
}
