using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetAllBranchs
{
    public class GetAllBranchsProfile : Profile
    {
        public GetAllBranchsProfile()
        {
            CreateMap<Branch, GetAllBranchsResult>();
        }
    }
}
