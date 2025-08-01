using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ShippingResRepository(WmDbContext dbContext) : GenericRepository<ShippingResEntity>(dbContext), IShippingResRepository
{
}
