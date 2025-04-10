using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public class SaleCancelledEventHandler : INotificationHandler<SaleCancelledEvent>
    {
        public Task Handle(SaleCancelledEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Venda cancelada: {notification.SaleNumber}");
            return Task.CompletedTask;
        }
    }
}
