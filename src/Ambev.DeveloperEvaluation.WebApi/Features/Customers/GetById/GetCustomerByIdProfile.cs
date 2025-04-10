using Ambev.DeveloperEvaluation.Application.Customers.Querys.GetById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetById
{
    public class GetCustomerByIdProfile : Profile
    {
        public GetCustomerByIdProfile()
        {
            CreateMap<GetCustomerByIdResult, GetCustomerByIdResponse>();
        }
    }
}
