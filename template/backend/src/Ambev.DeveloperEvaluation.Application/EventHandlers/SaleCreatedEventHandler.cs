using System.Text.Json;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.EventHandlers
{
    public class SaleCreatedEventHandler : INotificationHandler<SaleCreatedEvent>
    {
        private readonly IEventLogRepository _eventLogRepository;

        public SaleCreatedEventHandler(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }

        public async Task Handle(SaleCreatedEvent notification, CancellationToken cancellationToken)
        {
            var eventData = JsonSerializer.Serialize(notification, new JsonSerializerOptions
            {
                WriteIndented = false,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            var eventLog = new EventLog(notification.GetType().Name, eventData);
            await _eventLogRepository.AddAsync(eventLog);
        }
    }
}
