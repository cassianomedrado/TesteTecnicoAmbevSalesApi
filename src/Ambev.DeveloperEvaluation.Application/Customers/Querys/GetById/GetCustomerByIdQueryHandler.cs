using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Querys.GetById
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResult>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetCustomerByIdResult> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);
                return _mapper.Map<GetCustomerByIdResult>(customer);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao buscar cliente: " + ex.Message);
            }
        }
    }
}
