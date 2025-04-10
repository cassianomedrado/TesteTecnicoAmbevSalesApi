namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetById
{
    public class GetProductByIdRequest
    {
        public Guid Id { get; set; }

        public GetProductByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}
