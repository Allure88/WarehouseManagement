using Microsoft.EntityFrameworkCore;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Persistence.Repositories;

public class ClientRepository(WmDbContext dbContext) : GenericRepository<ClientEntity>(dbContext), IClientRepository
{
    public async Task<ClientEntity?> GetByName(string name)
    {
        return await _dbContext.Clients.Where(c => c.Name == name).FirstOrDefaultAsync();
    }

    public async Task<ClientEntity?> GetByNameWithDependents(string name)
    {
        return await _dbContext.Clients
            .Where(c => c.Name == name)
            .Include(c=>c.ShippingDocuments)
            .FirstOrDefaultAsync();
    }
}
