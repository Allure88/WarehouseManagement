using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IClientRepository : IGenericRepository<ClientEntity>
{
    Task<ClientEntity?> GetByName(string name);
}