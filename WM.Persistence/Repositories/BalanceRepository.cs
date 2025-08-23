using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Persistence.Repositories;

public class BalanceRepository(WmDbContext dbContext) : GenericRepository<BalanceEntity>(dbContext), IBalanceRepository
{
    public async Task<List<BalanceEntity>> GetAllWithDependencies()
    {
        return await _dbContext.Balances
            .Include(b => b.UnitOfMeasurement)
            .Include(b => b.Resource)
            .ToListAsync();
    }

    public async Task<BalanceEntity?> GetByPair(string unitName, string resourceName)
    {
        return await _dbContext.Balances
            .Include(b => b.UnitOfMeasurement)
            .Include(b => b.Resource)
            .FirstOrDefaultAsync(b => b.UnitOfMeasurement.Name == unitName && b.Resource.Name == resourceName);

    }
}
