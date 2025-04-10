using Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetById;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetById
{
    public class GetBranchByIdProfile : Profile
    {
        public GetBranchByIdProfile()
        {
            CreateMap<GetBranchByIdResult, GetBranchByIdResponse>();
        }
    }
}
