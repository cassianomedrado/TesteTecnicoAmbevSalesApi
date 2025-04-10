using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Branchs.Commands.CreateBranch
{
    public class CreateBranchProfile : Profile
    {
        public CreateBranchProfile()
        {
            CreateMap<Branch, CreateBranchResult>();
        }
    }
}
