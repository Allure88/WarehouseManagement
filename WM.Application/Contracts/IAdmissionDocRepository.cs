using WM.Domain.Entities;

namespace WM.Application.Contracts;

public interface IAdmissionDocRepository : IGenericRepository<AdmissionDocEntity>
{
    Task<AdmissionDocEntity?> GetByNumber(string number);
}
