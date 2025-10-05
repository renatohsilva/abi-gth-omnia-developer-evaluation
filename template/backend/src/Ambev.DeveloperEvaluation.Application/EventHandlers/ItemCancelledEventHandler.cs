using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Newtonsoft.Json;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.EventHandlers;

public class ItemCancelledEventHandler : INotificationHandler<ItemCancelledEvent>
{
    private readonly IEventLogRepository _eventLogRepository;

    public ItemCancelledEventHandler(IEventLogRepository eventLogRepository)
    {
        _eventLogRepository = eventLogRepository;
    }

    public async Task Handle(ItemCancelledEvent notification, CancellationToken cancellationToken)
    {
        var eventData = JsonConvert.SerializeObject(notification);
        var eventLog = new EventLog(notification.GetType().Name, eventData);
        await _eventLogRepository.AddAsync(eventLog);
    }
}
