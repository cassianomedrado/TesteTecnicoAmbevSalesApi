using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa um produto disponível para venda no sistema.
    /// </summary>
    public class Product : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public void Update(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        /// <summary>
        /// Valida a entidade.
        /// </summary>
        public ValidationResultDetail Validate()
        {
            var validator = new ProductValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
