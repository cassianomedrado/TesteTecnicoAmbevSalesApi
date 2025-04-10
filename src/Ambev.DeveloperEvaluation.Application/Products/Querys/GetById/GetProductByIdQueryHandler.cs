using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _repository.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<GetProductByIdResult>(product);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao buscar produto: " + ex.Message);
            }
        }
    }
}
