using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ShippingDocRepository(WmDbContext dbContext) : GenericRepository<ShippingDocEntity>(dbContext), IShippingDocRepository
{
    public async Task<ShippingDocEntity?> GetByNumber(string number)
    {
        return await _dbContext.ShippingDocs.Where(c => c.Number == number).FirstOrDefaultAsync();
    }
}
