using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IResourceRepository : IGenericRepository<ResourceEntity>
{
    Task<ResourceEntity?> GetByName(string name);

    Task<ResourceEntity?> GetByNameWithDependents(string name);
}
