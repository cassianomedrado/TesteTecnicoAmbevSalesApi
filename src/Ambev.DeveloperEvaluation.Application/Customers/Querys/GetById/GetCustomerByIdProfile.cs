using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.Querys.GetById
{
    public class GetCustomerByIdProfile : Profile
    {
        public GetCustomerByIdProfile()
        {
            CreateMap<Customer, GetCustomerByIdResult>();
        }
    }
}
