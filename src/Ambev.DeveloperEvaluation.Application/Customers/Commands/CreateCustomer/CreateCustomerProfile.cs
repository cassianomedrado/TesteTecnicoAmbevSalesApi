using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.Commands.CreateCustomer
{
    public class CreateCustomerProfile : Profile
    {
        public CreateCustomerProfile()
        {
            CreateMap<Customer, CreateCustomerResult>();
        }
    }
}
