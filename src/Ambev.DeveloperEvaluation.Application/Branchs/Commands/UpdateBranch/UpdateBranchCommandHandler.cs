using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Commands.UpdateBranch
{
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchResult>
    {
        private readonly IBranchRepository _repository;

        public UpdateBranchCommandHandler(IBranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateBranchResult> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _repository.GetByIdAsync(command.Id, cancellationToken);
                if (branch == null)
                {
                    return new UpdateBranchResult
                    {
                        Success = false,
                        Message = "Filial não encontrada."
                    };
                }

                branch.Update(command.Name, command.Address);
                await _repository.UpdateAsync(branch, cancellationToken);

                return new UpdateBranchResult
                {
                    Success = true,
                    Message = "Filial atualizada com sucesso."
                };

            }
            catch (Exception ex)
            {
                return new UpdateBranchResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
