using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetById
{
    public class GetBranchByIdProfile : Profile
    {
        public GetBranchByIdProfile()
        {
            CreateMap<Branch, GetBranchByIdResult>();
        }
    }
}
