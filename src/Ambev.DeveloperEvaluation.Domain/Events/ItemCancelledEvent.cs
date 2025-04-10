using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class ItemCancelledEvent : INotification
    {
        public string SaleNumber { get; }
        public Guid ProductId { get; }

        public ItemCancelledEvent(string saleNumber, Guid productId)
        {
            SaleNumber = saleNumber;
            ProductId = productId;
        }
    }
}
