using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Events
{
    public class SaleCancelledEvent : INotification
    {
        public string SaleNumber { get; }

        public SaleCancelledEvent(string saleNumber)
        {
            SaleNumber = saleNumber;
        }
    }
}
