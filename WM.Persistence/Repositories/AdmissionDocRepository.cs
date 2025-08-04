using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class AdmissionDocRepository(WmDbContext dbContext) : GenericRepository<AdmissionDocEntity>(dbContext), IAdmissionDocRepository
{
    public async Task<AdmissionDocEntity?> GetByNumber(string number)
    {
        return await _dbContext.AdmissionDocs
            .Where(d => d.Number == number)
            .Include(d=>d.AdmissionRes)
            .ThenInclude(ar => ar.UnitOfMeasurement)
            .Include(d => d.AdmissionRes)
            .ThenInclude(ar => ar.Resource)
            .ThenInclude(r=>r.Balances)
            .FirstOrDefaultAsync();
    }
}
