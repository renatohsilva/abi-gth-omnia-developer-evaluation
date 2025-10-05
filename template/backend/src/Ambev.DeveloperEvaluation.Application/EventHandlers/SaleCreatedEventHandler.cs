using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Newtonsoft.Json;
 using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.EventHandlers;

public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
{
    private readonly IEventLogRepository _eventLogRepository;

    public SaleCreatedEventHandler(IEventLogRepository eventLogRepository)
    {
        _eventLogRepository = eventLogRepository;
    }

    public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
    {
        var eventData = JsonConvert.SerializeObject(notification);
        var eventLog = new EventLog(notification.GetType().Name, eventData);
        await _eventLogRepository.AddAsync(eventLog);
    }
}
