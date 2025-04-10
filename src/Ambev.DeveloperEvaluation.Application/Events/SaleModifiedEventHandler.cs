using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public class SaleModifiedEventHandler : INotificationHandler<SaleModifiedEvent>
    {
        public Task Handle(SaleModifiedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Venda {notification.SaleNumber} foi modificada em {notification.UpdatedAt}");
            return Task.CompletedTask;
        }
    }
}
