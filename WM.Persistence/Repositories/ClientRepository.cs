using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ClientRepository(WmDbContext dbContext) : GenericRepository<ClientEntity>(dbContext), IClientRepository
{
}
