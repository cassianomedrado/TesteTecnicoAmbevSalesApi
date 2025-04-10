using Ambev.DeveloperEvaluation.Application.Products.Querys.GetAllProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
    public class GetAllProductsProfile : Profile
    {
        public GetAllProductsProfile()
        {
            CreateMap<GetAllProductsResult, GetAllProductsResponse>();
        }
    }

}
