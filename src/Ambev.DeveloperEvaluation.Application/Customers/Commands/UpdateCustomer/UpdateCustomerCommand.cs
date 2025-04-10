using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerResult> 
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
    }
}
