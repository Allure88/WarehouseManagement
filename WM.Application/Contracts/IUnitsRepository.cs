using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IUnitsRepository : IGenericRepository<UnitEntity>
{
    Task<UnitEntity?> GetByName(string name);
    Task<UnitEntity?> GetByNameWithDependents(string name);

}
