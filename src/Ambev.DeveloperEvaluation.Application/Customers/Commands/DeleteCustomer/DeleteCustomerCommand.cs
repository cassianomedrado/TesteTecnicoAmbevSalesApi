using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.DeleteCustomer
{
    public class DeleteCustomerCommand : IRequest<DeleteCustomerResult>
    {
        public Guid Id { get; set; }

        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }
    }

}
