using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetById
{
    public class GetProductByIdQuery : IRequest<GetProductByIdResult>
    {
        public Guid Id { get; set; }

        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
