using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResult>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteCustomerResult> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

                return new DeleteCustomerResult
                {
                    Success = deleted,
                    Message = deleted ? "Cliente removido com sucesso." : "Cliente não encontrado."
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao deletar cliente: " + ex.Message);
            }
        }
    }
}
