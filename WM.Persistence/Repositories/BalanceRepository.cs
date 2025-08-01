using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class BalanceRepository(WmDbContext dbContext) : GenericRepository<BalanceEntity>(dbContext), IBalanceRepository
{
}
