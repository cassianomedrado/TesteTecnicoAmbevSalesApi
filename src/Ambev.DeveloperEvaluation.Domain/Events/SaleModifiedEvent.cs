using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleModifiedEvent : INotification
    {
        public string SaleNumber { get; }
        public DateTime UpdatedAt { get; }

        public SaleModifiedEvent(string saleNumber, DateTime updatedAt)
        {
            SaleNumber = saleNumber;
            UpdatedAt = updatedAt;
        }
    }
}
