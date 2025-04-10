using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<GetAllProductsResult>> { }
}
