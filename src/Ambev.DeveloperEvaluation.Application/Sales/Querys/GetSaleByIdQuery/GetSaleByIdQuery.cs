using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Querys.GetSaleByIdQuery
{
    public class GetSaleByIdQuery : IRequest<GetSaleByIdResult>
    {
        public Guid Id { get; }

        public GetSaleByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
