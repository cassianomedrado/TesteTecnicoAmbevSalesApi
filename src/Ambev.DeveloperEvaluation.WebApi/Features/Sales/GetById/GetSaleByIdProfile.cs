using Ambev.DeveloperEvaluation.Application.Sales.Querys.GetSaleByIdQuery;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdProfile : Profile
    {
        public GetSaleByIdProfile()
        {       
            CreateMap<GetSaleByIdRequest, GetSaleByIdQuery>();
            CreateMap<GetSaleByIdResult, GetSaleByIdResponse>();
            CreateMap<GetSaleByIdResultItemResult, SaleItemByIdResponse>();
        }
    }
}
