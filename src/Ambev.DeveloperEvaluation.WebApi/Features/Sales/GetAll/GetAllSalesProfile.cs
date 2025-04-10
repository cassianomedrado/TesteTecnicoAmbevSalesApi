using Ambev.DeveloperEvaluation.Application.Sales.Querys.GetAllSalesQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll
{
    public class GetAllSalesProfile : Profile
    {
        public GetAllSalesProfile()
        {
            CreateMap<GetAllSalesResult, GetAllSalesResponse>();
            CreateMap<GetAllSalesItemResult, SaleItemResponse>();
        }
    }
}