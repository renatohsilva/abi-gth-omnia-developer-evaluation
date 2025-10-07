using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class EventLogRepository : IEventLogRepository
    {
        private readonly DefaultContext _context;

        public EventLogRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventLog eventLog)
        {
            await _context.EventLogs.AddAsync(eventLog);
            await _context.SaveChangesAsync();
        }
    }
}
