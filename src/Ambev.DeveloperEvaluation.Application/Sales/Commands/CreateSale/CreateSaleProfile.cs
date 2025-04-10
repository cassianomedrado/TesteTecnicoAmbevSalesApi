using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Items, opt => opt.Ignore())
                .ForMember(dest => dest.SaleDate, opt => opt.MapFrom(src =>
                    DateTime.SpecifyKind(src.SaleDate, DateTimeKind.Utc)));

            CreateMap<SaleItemDto, SaleItem>();

            CreateMap<Sale, CreateSaleResult>()
                .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
