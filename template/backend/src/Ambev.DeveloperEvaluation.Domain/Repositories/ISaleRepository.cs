using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories
{
    public interface ISaleRepository
    {
        Task AddAsync(Sale sale);
        Task UpdateAsync(Sale sale);
        Task<Sale> GetByIdAsync(Guid id);
        Task<IEnumerable<Sale>> GetAllAsync();
    }
}
