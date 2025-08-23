using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IBalanceRepository : IGenericRepository<BalanceEntity>
{
    Task<List<BalanceEntity>> GetAllWithDependencies();
    Task<BalanceEntity?> GetByPair(string unitName, string resourceName);
}
