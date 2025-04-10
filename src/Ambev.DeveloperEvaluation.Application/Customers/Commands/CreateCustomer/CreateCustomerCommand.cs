using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
    }
}
