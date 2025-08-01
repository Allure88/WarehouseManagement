using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class AdmissionDocRepository(WmDbContext dbContext) : GenericRepository<AdmissionDocEntity>(dbContext), IAdmissionDocRepository
{
}
