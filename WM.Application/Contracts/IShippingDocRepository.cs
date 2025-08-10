using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IShippingDocRepository : IGenericRepository<ShippingDocEntity>
{
    Task<ShippingDocEntity?> GetByNumber(string number);
    Task<List<ShippingDocEntity>> GetAllWithDependents();

}
