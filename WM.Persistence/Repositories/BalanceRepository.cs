using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class BalanceRepository(WmDbContext dbContext) : GenericRepository<BalanceEntity>(dbContext), IBalanceRepository
{
    public Task<List<BalanceEntity>> GetAllWithDependencies()
    {
        return _dbContext.Balances
            .Include(b => b.UnitOfMeasurement)
            .Include(b => b.Resource)
            .ToListAsync();
    }
}
