namespace Ambev.DeveloperEvaluation.Application.Customers.Querys.GetById
{
    public class GetCustomerByIdResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
    }
}
