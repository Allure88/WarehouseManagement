using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class StateRepository(WmDbContext dbContext) : GenericRepository<StateEntity>(dbContext), IStateRepository
{
}
