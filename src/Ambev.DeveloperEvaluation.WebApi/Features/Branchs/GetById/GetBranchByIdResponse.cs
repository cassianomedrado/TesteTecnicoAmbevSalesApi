namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetById
{
    public class GetBranchByIdResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
