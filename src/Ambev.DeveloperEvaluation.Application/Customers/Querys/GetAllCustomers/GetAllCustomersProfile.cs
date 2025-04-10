using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Customers.Querys.GetAllCustomers
{
    public class GetAllCustomersProfile : Profile
    {
        public GetAllCustomersProfile()
        {
            CreateMap<Customer, GetAllCustomersResult>();
        }
    }
}
