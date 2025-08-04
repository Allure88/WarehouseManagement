using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class UnitsRepository(WmDbContext dbContext) : GenericRepository<UnitEntity>(dbContext), IUnitsRepository
{
    public async Task<UnitEntity?> GetByName(string name)
    {
        return await _dbContext.UnitsOfMeasurement.Where(c => c.Name == name).FirstOrDefaultAsync();
    }

    public async Task<UnitEntity?> GetByNameWithDependents(string name)
    {
        return await _dbContext.UnitsOfMeasurement
            .Where(r => r.Name == name)
            .Include(r => r.Balances)
            .Include(r => r.AdmissionMovements)
            .Include(r => r.ShippingMovements)
            .FirstOrDefaultAsync();
    }
}
