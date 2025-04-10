using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Querys.GetAllSalesQuery
{
    public class GetAllSalesQuery : IRequest<List<GetAllSalesResult>> { }
}
