using Ambev.DeveloperEvaluation.Application.Branchs.Commands.CreateBranch;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch
{
    public class CreateBranchProfile : Profile
    {
        public CreateBranchProfile()
        {
            CreateMap<CreateBranchRequest, CreateBranchCommand>();
            CreateMap<CreateBranchResult, CreateBranchResponse>();
        }
    }
}
