namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetById
{
    public class GetBranchByIdRequest
    {
        public Guid Id { get; set; }

        public GetBranchByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}
