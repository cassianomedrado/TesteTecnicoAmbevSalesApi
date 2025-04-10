using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<GetAllProductsResult>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProductsResult>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _repository.GetAllAsync(cancellationToken);
                return products.Select(_mapper.Map<GetAllProductsResult>).ToList();
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao buscar produtos: " + ex.Message);
            }
        }
    }
}
