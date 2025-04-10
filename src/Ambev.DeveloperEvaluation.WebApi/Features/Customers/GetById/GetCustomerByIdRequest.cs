namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetById
{
    public class GetCustomerByIdRequest
    {
        public Guid Id { get; set; }

        public GetCustomerByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}
