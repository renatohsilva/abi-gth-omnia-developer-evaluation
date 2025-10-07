using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class EventLog : BaseEntity
    {
        public string EventType { get; private set; }
        public string EventData { get; private set; }
        public DateTime Timestamp { get; private set; }

        public EventLog(string eventType, string eventData)
        {
            EventType = eventType;
            EventData = eventData;
            Timestamp = DateTime.UtcNow;
        }

        // Required for EF Core
        protected EventLog() { }
    }
}
