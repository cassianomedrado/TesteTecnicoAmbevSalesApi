using Ambev.DeveloperEvaluation.Application.Customers.Querys.GetAllCustomers;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetAllCustomers
{
    public class GetAllCustomersProfile : Profile
    {
        public GetAllCustomersProfile()
        {
            CreateMap<GetAllCustomersResult, GetAllCustomersResponse>();
        }
    }
}