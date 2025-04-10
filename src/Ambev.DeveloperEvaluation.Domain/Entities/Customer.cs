using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Document { get; private set; }

        public Customer(string name, string email, string phone, string document)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }

        public void Update(string name, string email, string phone, string document)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }

    }
}
