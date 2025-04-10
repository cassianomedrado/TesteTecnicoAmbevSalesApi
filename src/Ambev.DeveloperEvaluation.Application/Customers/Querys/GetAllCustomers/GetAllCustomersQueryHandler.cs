using Ambev.DeveloperEvaluation.Application.Customers.Querys.GetAllCustomers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<GetAllCustomersResult>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetAllCustomersQueryHandler(ICustomerRepository saleRepository, IMapper mapper)
        {
            _customerRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllCustomersResult>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync(cancellationToken);
                return customers.Select(_mapper.Map<GetAllCustomersResult>).ToList();
            }        
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao buscar clientes: " + ex.Message);
            }

        }
    }
}
