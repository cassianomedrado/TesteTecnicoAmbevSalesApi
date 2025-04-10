namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer
{
    public class UpdateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
    }
}
