using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Querys.GetAllCustomers
{
    public class GetAllCustomersQuery : IRequest<List<GetAllCustomersResult>> { }
}
