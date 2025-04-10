using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleRequest, CreateSaleCommand>()
                .ForMember(dest => dest.SaleDate, opt =>
                    opt.MapFrom(src => DateTime.SpecifyKind(src.SaleDate, DateTimeKind.Utc)));

            CreateMap<CreateSaleItemRequest, SaleItemDto>();
            CreateMap<CreateSaleResult, CreateSaleResponse>();
        }
    }
}
