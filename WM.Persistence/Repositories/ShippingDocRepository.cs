using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ShippingDocRepository(WmDbContext dbContext) : GenericRepository<ShippingDocEntity>(dbContext), IShippingDocRepository
{
    public async Task<List<ShippingDocEntity>> GetAllWithDependents()
    {
        return await _dbContext.ShippingDocs
          .Include(d=>d.Client)
          .Include(d => d.ShippingRes)
          .ThenInclude(ar => ar.UnitOfMeasurement)
          .Include(d => d.ShippingRes)
          .ThenInclude(ar => ar.Resource)
          .ThenInclude(r => r.Balances)
          .ToListAsync();
    }

    public async Task<ShippingDocEntity?> GetByNumber(string number)
    {
        return await _dbContext.ShippingDocs
           .Where(d => d.Number == number)
           .Include(d => d.Client)
           .Include(d => d.ShippingRes)
           .ThenInclude(ar => ar.UnitOfMeasurement)
           .Include(d => d.ShippingRes)
           .ThenInclude(ar => ar.Resource)
           .ThenInclude(r => r.Balances)
           .FirstOrDefaultAsync();
    }
}
