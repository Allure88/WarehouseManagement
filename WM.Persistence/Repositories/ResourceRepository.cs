using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ResourceRepository(WmDbContext dbContext) : GenericRepository<ResourceEntity>(dbContext), IResourceRepository
{
    public async Task<ResourceEntity?> GetByName(string name)
    {
        return await _dbContext.Resources.Where(c => c.Name == name).FirstOrDefaultAsync();
    }

    public async Task<ResourceEntity?> GetByNameWithDependents(string name)
    {
        return await _dbContext.Resources
            .Where(r => r.Name == name)
            .Include(r=>r.Balances)
            .Include(r=>r.AdmissionMovements)
            .Include(r=>r.ShippingMovements)
            .FirstOrDefaultAsync();
    }

}