using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface IEventLogRepository
    {
        Task AddAsync(EventLog eventLog);
    }
}
