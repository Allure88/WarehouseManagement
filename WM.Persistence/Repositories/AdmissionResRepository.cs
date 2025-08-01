using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class AdmissionResRepository(WmDbContext dbContext) : GenericRepository<AdmissionResEntity>(dbContext), IAdmissionResRepository
{
}
