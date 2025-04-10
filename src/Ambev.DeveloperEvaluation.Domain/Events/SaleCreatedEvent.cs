using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCreatedEvent : INotification
    {
        public string SaleNumber { get; }
        public DateTime SaleDate { get; }

        public SaleCreatedEvent(string saleNumber, DateTime saleDate)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
        }
    }
}
