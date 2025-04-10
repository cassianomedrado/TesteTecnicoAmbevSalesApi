using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Querys.GetById
{
    public class GetCustomerByIdQuery : IRequest<GetCustomerByIdResult>
    {
        public Guid Id { get; }

        public GetCustomerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
