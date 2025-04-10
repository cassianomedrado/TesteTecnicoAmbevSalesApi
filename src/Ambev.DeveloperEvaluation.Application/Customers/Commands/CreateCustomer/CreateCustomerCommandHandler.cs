using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = new Customer(request.Name, request.Email, request.Phone, request.Document);
                var customerCreated = await _repository.CreateAsync(customer, cancellationToken);

                return _mapper.Map<CreateCustomerResult>(customerCreated);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao criar cliente: " + ex.Message);
            }
        }
    }
}
