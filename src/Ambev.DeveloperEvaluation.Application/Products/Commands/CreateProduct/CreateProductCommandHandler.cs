using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = new Product(request.Name, request.Description, request.Price);
                await _repository.CreateAsync(product, cancellationToken);

                return _mapper.Map<CreateProductResult>(product);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao criar produto: " + ex.Message);
            }
        }
    }
}
