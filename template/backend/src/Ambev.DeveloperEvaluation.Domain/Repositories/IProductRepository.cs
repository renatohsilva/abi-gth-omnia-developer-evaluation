using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository
{
    Task<Product> CreateAsync(Product product);
    Task<Product?> GetByIdAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<Product>UpdateAsync(Product product);
    Task<IEnumerable<Product>> GetAllAsync();
}
