using Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetAllBranchs;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetAllBranchs
{
    public class GetAllBranchsProfile : Profile
    {
        public GetAllBranchsProfile()
        {
            CreateMap<GetAllBranchsResult, GetAllBranchsResponse>();
        }
    }

}
