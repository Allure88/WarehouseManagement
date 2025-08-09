using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IBalanceRepository : IGenericRepository<BalanceEntity>
{
    Task<List<BalanceEntity>> GetAllWithDependencies();
}
