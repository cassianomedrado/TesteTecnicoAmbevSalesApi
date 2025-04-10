using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.Querys.GetById
{
    public class GetProductByIdProfile : Profile
    {
        public GetProductByIdProfile()
        {
            CreateMap<Product, GetProductByIdResult>();
        }
    }
}
