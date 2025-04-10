namespace Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetById
{
    public class GetBranchByIdResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}
