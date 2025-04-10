using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Querys.GetAllSalesQuery
{
    public class GetAllSalesProfile : Profile
    {
        public GetAllSalesProfile()
        {
            CreateMap<Sale, GetAllSalesResult>();
        }
    }
}
