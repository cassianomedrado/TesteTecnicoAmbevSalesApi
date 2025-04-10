using Ambev.DeveloperEvaluation.Application.Customers.Commands.UpdateCustomer;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer
{
    public class UpdateCustomerProfile : Profile
    {
        public UpdateCustomerProfile()
        {
            CreateMap<UpdateCustomerRequest, UpdateCustomerCommand>();
        }
    }
}
