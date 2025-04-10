using Ambev.DeveloperEvaluation.Domain.Events;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Events
{
    public class ItemCancelledEventHandler : INotificationHandler<ItemCancelledEvent>
    {
        public Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"Item {notification.ProductId} removido da venda {notification.SaleNumber}");
            return Task.CompletedTask;
        }
    }
}
