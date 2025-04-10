﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer
{
    public class CreateCustomerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Document { get; set; } = string.Empty;
    }
}
