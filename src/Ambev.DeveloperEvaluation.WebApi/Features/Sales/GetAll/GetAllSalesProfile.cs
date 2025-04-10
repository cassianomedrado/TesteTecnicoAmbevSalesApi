using Ambev.DeveloperEvaluation.Application.Sales.Querys.GetAllSalesQuery;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll
{
    public class GetAllSalesProfile : Profile
    {
        public GetAllSalesProfile()
        {
            CreateMap<GetAllSalesRequest, GetAllSalesQuery>();
            CreateMap<GetAllSalesResult, GetAllSalesResponse>();
        }
    }
}