using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Commands.DeleteBranch
{
    public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchResult>
    {
        private readonly IBranchRepository _repository;

        public DeleteBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteBranchResult> Handle(DeleteBranchCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

                return new DeleteBranchResult
                {
                    Success = deleted,
                    Message = deleted ? "Filial removida com sucesso." : "Filial não encontrada."
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao deletar filial: " + ex.Message);
            }
        }
    }
}
