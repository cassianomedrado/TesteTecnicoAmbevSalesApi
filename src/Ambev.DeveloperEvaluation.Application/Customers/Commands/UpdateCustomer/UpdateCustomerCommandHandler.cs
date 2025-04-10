using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(command.Id, cancellationToken);
                if (customer == null)
                {
                    return new UpdateCustomerResult
                    {
                        Success = false,
                        Message = "Cliente não encontrado."
                    };
                }

                customer.Update(command.Name, command.Email, command.Phone, command.Document);
                await _repository.UpdateAsync(customer, cancellationToken);

                return new UpdateCustomerResult
                {
                    Success = true,
                    Message = "Cliente atualizado com sucesso."
                };
            }
            catch(Exception ex)
            {
                return new UpdateCustomerResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }           
        }
    }
}
