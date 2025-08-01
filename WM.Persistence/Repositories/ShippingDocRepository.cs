using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ShippingDocRepository(WmDbContext dbContext) : GenericRepository<ShippingDocEntity>(dbContext), IShippingDocRepository
{
}
