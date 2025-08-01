using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class UnitsRepository(WmDbContext dbContext) : GenericRepository<UnitEntity>(dbContext), IUnitsRepository
{
}
