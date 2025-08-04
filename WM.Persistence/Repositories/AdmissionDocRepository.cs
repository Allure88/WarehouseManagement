using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class AdmissionDocRepository(WmDbContext dbContext) : GenericRepository<AdmissionDocEntity>(dbContext), IAdmissionDocRepository
{
    public async Task<AdmissionDocEntity?> GetByNumber(string number)
    {
        return await _dbContext.AdmissionDocs.Where(c => c.Number == number).FirstOrDefaultAsync();
    }
}
