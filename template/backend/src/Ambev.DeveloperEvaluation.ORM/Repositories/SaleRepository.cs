using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _context;

    public SaleRepository(DefaultContext context)
    {
        _context = context;
    }

    public async Task<Sale> CreateAsync(Sale sale)
    {
        await _context.Sales.AddAsync(sale);
        await _context.SaveChangesAsync();
        return sale;
    }

    public async Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales.Include(s => s.Items).AsNoTracking().ToListAsync();
    }

    public async Task<Sale> GetByIdAsync(Guid id)
    {
        return await _context.Sales.Include(s => s.Items).FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Sale> UpdateAsync(Sale sale)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync();
        return sale;
    }
}
