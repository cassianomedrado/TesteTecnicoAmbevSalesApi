using Ambev.DeveloperEvaluation.Application.Products.Querys.GetById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetById
{
    public class GetProductByIdProfile : Profile
    {
        public GetProductByIdProfile()
        {
            CreateMap<GetProductByIdResult, GetProductByIdResponse>();
        }
    }
}
