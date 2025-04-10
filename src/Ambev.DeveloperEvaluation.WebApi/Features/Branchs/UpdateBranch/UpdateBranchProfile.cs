using Ambev.DeveloperEvaluation.Application.Branchs.Commands.UpdateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.UpdateBranch
{
    public class UpdateBranchProfile : Profile
    {
        public UpdateBranchProfile()
        {
            CreateMap<UpdateBranchRequest, UpdateBranchCommand>();
        }
    }
}
