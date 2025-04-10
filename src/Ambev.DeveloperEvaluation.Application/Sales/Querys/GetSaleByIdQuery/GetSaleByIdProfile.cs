using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Querys.GetSaleByIdQuery
{
    public class GetSaleByIdProfile : Profile
    {
        public GetSaleByIdProfile()
        {
            CreateMap<Sale, GetSaleByIdResult>();
            CreateMap<SaleItem, GetSaleByIdResultItemResult>();
        }
    }
}
