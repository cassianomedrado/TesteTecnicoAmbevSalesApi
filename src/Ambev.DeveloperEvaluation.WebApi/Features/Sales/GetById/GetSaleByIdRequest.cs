namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdRequest
    {
        public Guid Id { get; set; }

        public GetSaleByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}