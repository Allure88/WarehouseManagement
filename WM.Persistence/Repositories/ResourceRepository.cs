using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ResourceRepository(WmDbContext dbContext) : GenericRepository<ResourceEntity>(dbContext), IResourceRepository
{
}