using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Representa um item dentro de uma venda.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        public Guid SaleId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }
        public Sale Sale { get; private set; }
        public Product Product { get; private set; }
        public decimal Discount { get; private set; }

        public SaleItem(Guid productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public void Update(int quantity, decimal unitPrice)
        {
            Quantity = quantity;
            UnitPrice = unitPrice;
            TotalPrice = quantity * unitPrice;
        }

        public void ApplyDiscount(decimal discount)
        {
            Discount = discount;
            TotalPrice = Quantity * UnitPrice * (1 - discount);
        }

        public void SetSaleId(Guid id)
        {
            SaleId = id;
        }
    }
}