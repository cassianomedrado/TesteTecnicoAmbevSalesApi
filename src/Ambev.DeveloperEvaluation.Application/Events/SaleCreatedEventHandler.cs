using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        public Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Venda criada: {notification.SaleNumber} em {notification.SaleDate}");
            return Task.CompletedTask;
        }
    }
}
